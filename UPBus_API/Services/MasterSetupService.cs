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
                    bus.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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
                    bus.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;

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

        #region Gate 9_Nov_2024

        public async Task<DataTable> GetGateList()
        {
            string sql = "SELECT * FROM Gate";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate data = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE GateCode=@gateCode", new SqlParameter("@gateCode", info.GateCode)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Gate No duplicate!";
                }
                else
                {

                    Gate gate = _mapper.Map<Gate>(info);
                    gate.CreatedDate = GetLocalStdDT();
                    gate.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.Gate.Add(gate);
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

        public async Task<ResponseMessage> UpdateGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE GateCode=@gateCode", new SqlParameter("@gateCode", info.GateCode)).SingleOrDefaultAsync();
                if (gate == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                    gate.GateName = info.GateName;
                    gate.Active = info.Active;
                    gate.UpdatedDate = GetLocalStdDT();
                    gate.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
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

        public async Task<ResponseMessage> DeleteGate(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE GateCode=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (gate == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                _context.Gate.Remove(gate);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Removed successfully.";
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent = $"An error occurred: {e.Message}";
            }

            return msg;
        }

        #endregion

        #region Expense Type 11-Nov-2024

        public async Task<DataTable> GetExpenseTypeList()
        {

            string sql = "SELECT * FROM ExpenseType";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> SaveExpenseType(ExpenseTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                ExpenseType expenseType = await _context.ExpenseType.FromSqlRaw("SELECT * FROM ExpenseType WHERE ExpCode=@expCode", new SqlParameter("@expCode", info.ExpCode)).SingleOrDefaultAsync();
                if (expenseType != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Expense No duplicate!";
                }
                else
                {

                    ExpenseType data = _mapper.Map<ExpenseType>(info);
                    data.CreatedDate = GetLocalStdDT();
                    data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.ExpenseType.Add(data);
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

        public async Task<ResponseMessage> UpdateExpenseType(ExpenseTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                ExpenseType expenseType = await _context.ExpenseType.FromSqlRaw("SELECT * FROM ExpenseType WHERE REPLACE(ExpName,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.ExpName)).SingleOrDefaultAsync();
                if (expenseType == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    expenseType.ExpName = info.ExpName;
                    expenseType.ExpType = info.ExpType;
                    expenseType.Active = info.Active;
                    expenseType.UpdatedDate = GetLocalStdDT();
                    expenseType.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.ExpenseType.Update(expenseType);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        public async Task<ResponseMessage> DeleteExpenseType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                ExpenseType expenseType = await _context.ExpenseType.FromSqlRaw("SELECT * FROM ExpenseType WHERE ExpCode=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (expenseType == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.ExpenseType.Remove(expenseType);

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

        #region Income Type 11-Nov-2024

        public async Task<DataTable> GetIncomeTypeList()
        {
            string sql = "SELECT * FROM IncomeType";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> SaveIncomeType(IncomeTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                IncomeType incomeType = await _context.IncomeType.FromSqlRaw("SELECT Top 1 * FROM IncomeType WHERE REPLACE(IncName,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.IncName)).SingleOrDefaultAsync();
                if (incomeType != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    IncomeType type = _mapper.Map<IncomeType>(info);
                    type.CreatedDate = GetLocalStdDT();
                    type.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.IncomeType.Add(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully added";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        public async Task<ResponseMessage> UpdateIncomeType(IncomeTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                IncomeType expenseType = await _context.IncomeType.FromSqlRaw("SELECT * FROM IncomeType WHERE REPLACE(IncName,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.IncName)).SingleOrDefaultAsync();
                if (expenseType == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    expenseType.IncName = info.IncName;
                    expenseType.IncType = info.IncType;
                    expenseType.Active = info.Active;
                    expenseType.UpdatedDate = GetLocalStdDT();
                    expenseType.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.IncomeType.Update(expenseType);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        public async Task<ResponseMessage> DeleteIncomeType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                IncomeType incomeType = await _context.IncomeType.FromSqlRaw("SELECT * FROM IncomeType WHERE IncCode=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (incomeType == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.IncomeType.Remove(incomeType);

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

        #region Trip Type 11-Nov-2024

        public async Task<DataTable> GetTripTypeList()
        {
            string sql = "SELECT * FROM TripType";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> SaveTripType(TripTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TripType tripType = await _context.TripType.FromSqlRaw("SELECT Top 1 * FROM TripType WHERE REPLACE(TripName,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.TripName)).SingleOrDefaultAsync();
                if (tripType != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    TripType type = _mapper.Map<TripType>(info);
                    type.CreatedDate = GetLocalStdDT();
                    type.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.TripType.Add(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully added";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        public async Task<ResponseMessage> UpdateTripType(TripTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TripType tripType = await _context.TripType.FromSqlRaw("SELECT * FROM TripType WHERE REPLACE(TripName,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.TripName)).SingleOrDefaultAsync();
                if (tripType == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    tripType.TripName = info.TripName;
                    tripType.TrpType = info.TrpType;
                    tripType.Active = info.Active;
                    tripType.UpdatedDate = GetLocalStdDT();
                    tripType.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.TripType.Update(tripType);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        public async Task<ResponseMessage> DeleteTripType(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                TripType trip = await _context.TripType.FromSqlRaw("SELECT * FROM TripType WHERE TripCode=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (trip == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                _context.TripType.Remove(trip);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Removed successfully.";
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent = $"An error occurred: {e.Message}";
            }

            return msg;
        }

        #endregion

        #region Daily Plan 23-Nov-2024

        public async Task<DailyPlanDto> GetDailyPlanId(string id)
        {
            DailyPlanDto dailyPlanDto = new DailyPlanDto();
            try
            {
                DailyPlan dailyPlan = await _context.DailyPlan.FromSqlRaw("SELECT * FROM DailyPlan WHERE DailyPlanId=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (dailyPlan != null)
                {
                    dailyPlanDto = _mapper.Map<DailyPlanDto>(dailyPlan);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return dailyPlanDto;
        }

        public async Task<string[]> GetOnlyTripCode()
        {
            return await _context.TripType
                .Where(t => t.Active == true)
                .Select(t => t.TripCode)
                .ToArrayAsync();

        }

        public async Task<List<BusDto>> GetBusNoList()
        {
            string sql = @"SELECT BusNo, DriverName FROM Bus WHERE Active = 1";
            DataTable dt = await GetDataTableAsync(sql);
            var busList = new List<BusDto>();

            foreach (DataRow row in dt.Rows)
            {
                busList.Add(new BusDto
                {
                    BusNo = row["BusNo"].ToString(),
                    DriverName = row["DriverName"].ToString()
                });
            }

            return busList;
        }

        public async Task<string> GetDriverByBusNo(string busNo)
        {
            string sql = @"SELECT DriverName FROM Bus WHERE BusNo = @busNo";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@busNo", busNo));

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["DriverName"].ToString();
            }
            return null; // Return null if no driver found
        }

        public async Task<DataTable> GetDailyPlanList()
        {
            string sql = @"SELECT * from DailyPlan";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> SaveDailyPlan(DailyPlanDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };

            try
            {
                // Fixing the parameter names and query logic
                DailyPlan dailyPlan = await _context.DailyPlan
                    .FromSqlRaw("SELECT Top 1 * FROM DailyPlan WHERE TripCode = @tripCode AND BusNo = @busNo AND TripDate = @tripDate AND TripTime = @tripTime",
                        new SqlParameter("@tripCode", info.TripCode),
                        new SqlParameter("@busNo", info.BusNo),
                        new SqlParameter("@tripDate", info.TripDate),
                        new SqlParameter("@tripTime", info.TripTime))
                    .SingleOrDefaultAsync();

                // Check if the data already exists (duplicate check)
                if (dailyPlan != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data is duplicate!";
                }
                else
                {
                    // Map DTO to Entity
                    DailyPlan data = _mapper.Map<DailyPlan>(info);
                    data.CreatedDate = GetLocalStdDT(); // Assuming this method gets the correct local date
                    data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser"; // Get current user

                    // Add new data to the context
                    _context.DailyPlan.Add(data);
                    await _context.SaveChangesAsync(); // Save changes to database

                    msg.Status = true;
                    msg.MessageContent = "Daily plan added successfully.";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent = "An error occurred while saving the data: " + e.Message;
            }

            return msg;
        }

        public async Task<ResponseMessage> UpdateDailyPlan(DailyPlanDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyPlan dailyPlan = await _context.DailyPlan.FromSqlRaw("SELECT * FROM DailyPlan WHERE REPLACE(DailyPlanID,' ','')=REPLACE(@id,' ','') ", new SqlParameter("@id", info.DailyPlanID)).SingleOrDefaultAsync();

                if (dailyPlan == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                    return msg;
                }
                else
                {
                    dailyPlan.TripCode = info.TripCode;
                    dailyPlan.TripDate = info.TripDate;
                    dailyPlan.TripTime = info.TripTime;
                    dailyPlan.Track = info.Track;
                    dailyPlan.BusNo = info.BusNo;
                    dailyPlan.DriverName = info.DriverName;
                    dailyPlan.UpdatedDate = GetLocalStdDT();
                    dailyPlan.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully updated";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }

        public async Task<ResponseMessage> DeleteDailyPlan(int id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyPlan dailyPlan = await _context.DailyPlan.FromSqlRaw("SELECT * FROM DailyPlan WHERE DailyPlanID=@id", new SqlParameter("@id", id)).FirstOrDefaultAsync();

                if (dailyPlan == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.DailyPlan.Remove(dailyPlan);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Removed";
                    return msg;
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
