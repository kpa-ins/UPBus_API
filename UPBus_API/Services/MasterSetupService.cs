using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using UPBus_API.DTOs;
using UPBus_API.Entities;

namespace UPBus_API.Services
{
    public class MasterSetupService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly string _conStr;

        public MasterSetupService(ApplicationDBContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _httpContextAccessor = httpContextAccessor;
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

        #region Bus 8_Nov_2024

        public async Task<DataTable> GetBusList()
        {
            string sql = "SELECT * FROM Bus";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateBus(BusDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Bus data = await _context.Bus.FromSqlRaw("SELECT * FROM Bus WHERE BusNo=@no", new SqlParameter("@no", info.BusNo)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Bus No duplicate!";
                }
                else
                {

                    Bus bus = _mapper.Map<Bus>(info);
                    bus.CreatedDate = GetLocalStdDT();
                    info.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                    _context.Bus.Add(bus);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully created!";

                }


            }

            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }




        public async Task<ResponseMessage> UpdateBus(BusDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Bus bus = await _context.Bus.FromSqlRaw("SELECT * FROM Bus WHERE BusNo=@no", new SqlParameter("@no", info.BusNo)).SingleOrDefaultAsync();
                if (bus == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                    bus.DriverName = info.DriverName;
                    bus.NoOfSeat = info.NoOfSeat;
                    bus.Owner = info.Owner;
                    bus.Active = info.Active;
                    bus.Remark = info.Remark;
                    bus.UpdatedDate = GetLocalStdDT();
                    bus.UpdatedUser = info.UpdatedUser;

                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated!";


                }
            }

            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        public async Task<ResponseMessage> DeleteBus(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Bus bus = await _context.Bus.FromSqlRaw("SELECT * FROM Bus WHERE BusNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (bus == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Bus.Remove(bus);

                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Deleted!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }


        #endregion


    }
}
