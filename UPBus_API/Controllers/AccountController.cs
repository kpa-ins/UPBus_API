using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using UPBus_API.DTOs;
using UPBus_API.JwtFeatures;
using UPBus_API.Services;

namespace UPBus_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly AccountService _service;
        private readonly ApplicationDBContext _context;



        public AccountController(UserManager<IdentityUser> userManager, IMapper mapper, JwtHandler jwtHandler, AccountService service, ApplicationDBContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _service = service;
            _context = context;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<IdentityUser>(userForRegistration);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var result = await _userManager.CreateAsync(user, userForRegistration.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }

                await _userManager.AddToRoleAsync(user, userForRegistration.Role);

                transaction.Commit();

                return StatusCode(201);
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return BadRequest(new RegistrationResponseDto { Errors = new[] { e.Message } });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            
            var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
          
            
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
            //return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, YardList = owner.YardCode, OwnerList = owner.OwnerID });

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            await _userManager.RemovePasswordAsync(user);
            var resetPassResult = await _userManager.AddPasswordAsync(user, resetPasswordDto.Password);

            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoleList()
        {
            DataTable dt = await _service.GetRoleList();
            return Ok(dt);
        }



        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            DataTable dt = await _service.GetUserList();

            return Ok(dt);
        }


        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,SysAdmin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            ResponseMessage msg = await _service.DeleteUser(id);
            return Ok(msg);
        }

    }
}
