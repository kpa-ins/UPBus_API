using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UPBus_API.DTOs;
using UPBus_API.Services;

namespace UPBus_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterSetupController(MasterSetupService service) : ControllerBase
    {
        private readonly MasterSetupService _service = service;

        [AllowAnonymous]
        [HttpGet]
        public string Test()
        {
            return "Testing...";

        }

        #region Bus 8-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetBusList()
        {
            DataTable dt = await _service.GetBusList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBus(BusDto info)
        {
            ResponseMessage msg = await _service.CreateBus(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBus(BusDto info)
        {
            ResponseMessage msg = await _service.UpdateBus(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(string id)
        {
            ResponseMessage msg = await _service.DeleteBus(id);
            return Ok(msg);
        }

        #endregion
    }
}
