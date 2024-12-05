using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UPBus_API.DTOs;
using UPBus_API.Entities;

namespace UPBus_API.Services
{
    public class DailyAccService
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly string _conStr;

        public DailyAccService(ApplicationDBContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
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




        #region Daily Gate Expense 5_Dec_2024

        public async Task<DataTable> GetDailyGateExpenseList()
        {
            string sql = "SELECT dge.*, ExpName FROM DailyGateExpense dge Inner Join ExpenseType e ON dge.ExpCode = e.ExpCode";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateDailyGateExpense(DailyGateExpenseDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                string code = info.GateCode + "-" + "E" + GetLocalStdDT().ToString("ddMMyy") + "-";
                DailyGateExpense expense = await _context.DailyGateExpense.FromSqlRaw("SELECT Top 1 * FROM DailyGateExpense WHERE ExpNo LIKE '" + code + "%' ORDER BY ExpNo DESC").SingleOrDefaultAsync();
                if (expense != null)
                {
                    int num = Convert.ToInt32(expense.ExpNo[^3..]);
                    num++;
                    info.ExpNo = code + num.ToString("D3");
                }
                else
                {
                    info.ExpNo = code + "001";
                }


                DailyGateExpense gateExpense = _mapper.Map<DailyGateExpense>(info);
                gateExpense.CreatedDate = GetLocalStdDT();
                gateExpense.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.DailyGateExpense.Add(gateExpense);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully created!";
               

            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        public async Task<ResponseMessage> UpdateDailyGateExpense(DailyGateExpenseDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateExpense expense = await _context.DailyGateExpense.FromSqlRaw("SELECT * FROM DailyGateExpense WHERE ExpNo=@code", new SqlParameter("@code", info.ExpNo)).SingleOrDefaultAsync();
                if (expense == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                    expense.ExpDate = info.ExpDate;
                    expense.ExpCode = info.ExpCode;
                    expense.PaidType = info.PaidType;
                    expense.Amount = info.Amount;
                    expense.Description = info.Description;
                    expense.Remark = info.Remark;
                    expense.UpdatedDate = GetLocalStdDT();
                    expense.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> DeleteDailyGateExpense(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateExpense expense = await _context.DailyGateExpense.FromSqlRaw("SELECT * FROM DailyGateExpense WHERE ExpNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (expense == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                _context.DailyGateExpense.Remove(expense);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Removed successfully.";
            }

            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        #endregion


        #region Daily Gate Income 5_Dec_2024

        public async Task<DataTable> GetDailyGateIncomeList()
        {
            string sql = "SELECT dgi.*, IncName FROM DailyGateIncome dgi Inner Join IncomeType i ON dgi.IncCode = i.IncCode";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateDailyGateIncome(DailyGateIncomeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                string code = info.GateCode + "-" + "E" + GetLocalStdDT().ToString("ddMMyy") + "-";
                DailyGateIncome income = await _context.DailyGateIncome.FromSqlRaw("SELECT Top 1 * FROM DailyGateIncome WHERE IncNo LIKE '" + code + "%' ORDER BY IncNo DESC").SingleOrDefaultAsync();
                if (income != null)
                {
                    int num = Convert.ToInt32(income.IncNo[^3..]);
                    num++;
                    info.IncNo = code + num.ToString("D3");
                }
                else
                {
                    info.IncNo = code + "001";
                }


                DailyGateIncome gateIncome = _mapper.Map<DailyGateIncome>(info);
                gateIncome.CreatedDate = GetLocalStdDT();
                gateIncome.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.DailyGateIncome.Add(gateIncome);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully created!";


            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        public async Task<ResponseMessage> UpdateDailyGateIncome(DailyGateIncomeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateIncome expense = await _context.DailyGateIncome.FromSqlRaw("SELECT * FROM DailyGateIncome WHERE IncNo=@code", new SqlParameter("@code", info.IncNo)).SingleOrDefaultAsync();
                if (expense == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                    expense.IncDate = info.IncDate;
                    expense.IncCode = info.IncCode;
                    expense.PaidType = info.PaidType;
                    expense.Amount = info.Amount;
                    expense.Remark = info.Remark;
                    expense.UpdatedDate = GetLocalStdDT();
                    expense.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> DeleteDailyGateIncome(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateIncome income = await _context.DailyGateIncome.FromSqlRaw("SELECT * FROM DailyGateIncome WHERE IncNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (income == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                _context.DailyGateIncome.Remove(income);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Removed successfully.";
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
