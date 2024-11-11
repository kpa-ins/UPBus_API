using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using UPBus_API.DTOs;

namespace UPBus_API.Services
{
    public class AccountService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _conStr;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDBContext context, IConfiguration configuration, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _userManager = userManager;
            _mapper = mapper;
            _context.Database.SetCommandTimeout(300);
        }

        public bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        public DateTime GetLocalStdDT()
        {
            if (!IsLinux)
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Myanmar Standard Time");
                return localTime;
            }
            else
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Asia/Yangon");
                DateTime pacific = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
                return pacific;
            }
        }

        //public static DateTime GetLocalStdDT()
        //{
        //	return DateTime.Now.ToLocalTime().AddMinutes(390);
        //}

        public Task<DataTable> GetDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using var newCon = new SqlConnection(_conStr);
                using var adapt = new SqlDataAdapter(sSQL, newCon);
                newCon.Open();
                adapt.SelectCommand.CommandType = CommandType.Text;
                if (para != null)
                    adapt.SelectCommand.Parameters.AddRange(para);

                DataTable dt = new();
                adapt.Fill(dt);
                newCon.Close();
                return dt;
            });
        }

        public async Task<DataTable> GetRoleList()
        {
            string sql = @"SELECT * FROM AspNetRoles where [Name] <> 'SysAdmin'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetUserList()
        {
            string sql = @"SELECT u.Id,u.Email, r.Name FROM AspNetUsers u
                           INNER JOIN AspNetUserRoles ar ON ar.UserId = u.Id
                           INNER JOIN AspNetRoles r ON r.Id = ar.RoleId WHERE u.Email != 'insadmin@gmail.com'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
         
        }



        public async Task<ResponseMessage> DeleteUser(string id)
        {
            ResponseMessage msg = new() { Status = false };
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully deleted!";
                    }
                    else
                    {
                        msg.MessageContent = "Something went wrong!";
                    }
                }
            }
            catch (Exception e)
            {
                msg.MessageContent = e.Message;
                return msg;
            }
            return msg;
        }



    }
}
