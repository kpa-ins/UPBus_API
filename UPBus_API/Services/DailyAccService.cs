using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime;
using UPBus_API.DTOs;
using UPBus_API.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<DataTable> GetDailyGateExpenseList(string id)
        {
            string sql = "SELECT dge.*, ExpName FROM DailyGateExpense dge Inner Join ExpenseType e ON dge.ExpCode = e.ExpCode WHERE ExpNo LIKE '"+ id+ "%'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateDailyGateExpense(DailyGateExpenseDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                string code = info.GateAccCode + "-";
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


                DailyGateExpense dailyGateExpense = await _context.DailyGateExpense.FromSqlRaw("SELECT * FROM DailyGateExpense WHERE ExpNo!=@code  AND REPLACE(ExpCode,' ','')=REPLACE(@exp,' ','') ", new SqlParameter("@code", info.ExpNo), new SqlParameter("@exp", info.ExpCode)).SingleOrDefaultAsync();

                if (dailyGateExpense != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Expense type already exists!";
                }
                else
                {
                    #region Update Daily Gate Acc

                    DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();

                    dailyGateAcc.ExpTotalAmt += info.Amount;

                    #endregion


                    DailyGateExpense gateExpense = _mapper.Map<DailyGateExpense>(info);
                    gateExpense.CreatedDate = GetLocalStdDT();
                    gateExpense.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                    _context.DailyGateExpense.Add(gateExpense);
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

                    DailyGateExpense dailyGateExpense = await _context.DailyGateExpense.FromSqlRaw("SELECT * FROM DailyGateExpense WHERE ExpNo!=@code  AND REPLACE(ExpCode,' ','')=REPLACE(@exp,' ','') ", new SqlParameter("@code", info.ExpNo), new SqlParameter("@exp", info.ExpCode)).SingleOrDefaultAsync();

                    if (dailyGateExpense != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Expense type already exists!";
                    }

                    else
                    {
                        #region Update Daily Gate Acc

                        DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();

                        dailyGateAcc.ExpTotalAmt -= expense.Amount;
                        dailyGateAcc.ExpTotalAmt += info.Amount;

                        #endregion

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
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        public async Task<ResponseMessage> DeleteDailyGateExpense(string id, string date)
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

                #region Update Daily Gate Acc
               
                DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", expense.GateCode), new SqlParameter("@date", date)).SingleOrDefaultAsync();

                dailyGateAcc.ExpTotalAmt -= expense.Amount;

                #endregion

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

        public async Task<DataTable> GetDailyGateIncomeList(string id)
        {
            string sql = "SELECT dgi.*, IncName FROM DailyGateIncome dgi Inner Join IncomeType i ON dgi.IncCode = i.IncCode WHERE IncNo LIKE '"+ id+ "%'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateDailyGateIncome(DailyGateIncomeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                string code = info.GateAccCode + "-";
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


                #region Update Daily Gate Acc

                DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();

                if(info.PaidType == "Paid")
                    dailyGateAcc.IncReceiveAmt += info.Amount;
                if (info.PaidType == "Credit")
                    dailyGateAcc.IncCreditAmt += info.Amount;

                #endregion


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
                DailyGateIncome income = await _context.DailyGateIncome.FromSqlRaw("SELECT * FROM DailyGateIncome WHERE IncNo=@code", new SqlParameter("@code", info.IncNo)).SingleOrDefaultAsync();
                if (income == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {
                    #region Update Daily Gate Acc

                    DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();

                    if(info.PaidType == "Paid")
                    {
                        if (info.PaidType == "Paid" && income.PaidType == "Paid")
                        {
                            dailyGateAcc.IncReceiveAmt -= income.Amount;
                            dailyGateAcc.IncReceiveAmt += info.Amount;
                        }
                        else if(info.PaidType == "Paid" && income.PaidType == "Credit")
                        {
                            dailyGateAcc.IncCreditAmt -= income.Amount;
                            dailyGateAcc.IncReceiveAmt += info.Amount;
                        }
                    }
                    else if(info.PaidType == "Credit")
                    {
                        if (info.PaidType == "Credit" && income.PaidType == "Credit")
                        {
                            dailyGateAcc.IncCreditAmt -= income.Amount;
                            dailyGateAcc.IncCreditAmt += info.Amount;
                        }
                        else if(info.PaidType == "Credit" && income.PaidType == "Paid")
                        {
                            dailyGateAcc.IncReceiveAmt -= income.Amount;
                            dailyGateAcc.IncCreditAmt += info.Amount;
                        }
                    }
                    
                    #endregion

                    income.IncDate = info.IncDate;
                    income.IncCode = info.IncCode;
                    income.PaidType = info.PaidType;
                    income.Amount = info.Amount;
                    income.Description = info.Description;
                    income.Remark = info.Remark;
                    income.UpdatedDate = GetLocalStdDT();
                    income.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> DeleteDailyGateIncome(string id, string date)
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

                #region Update Daily Gate Acc

                DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code  AND Cast(AccDate as date) =@date", new SqlParameter("@code", income.GateCode), new SqlParameter("@date", date)).SingleOrDefaultAsync();

                if(income.PaidType =="Paid")
                    dailyGateAcc.IncReceiveAmt -= income.Amount;
                if(income.PaidType == "Credit")
                    dailyGateAcc.IncCreditAmt -= income.Amount;

                #endregion

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



        #region Daily Gate Acc 6_Dec_2024

        public async Task<DataTable> GetDailyGateAccList()
        {
            string sql = "SELECT * FROM DailyGateAcc";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetDailyGateAccById(string gate, string date)
        {
            string sql = "SELECT * FROM DailyGateAcc WHERE GateCode=@gate AND Cast(AccDate as date)=@date";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@gate", gate), new SqlParameter("@date", date));
            return dt;
        }

        public async Task<ResponseMessage> CreateDailyGateAcc(DailyGateAccDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateAcc data = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code AND Cast(AccDate as Date) =@date ", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data already exists!";
                }
                else
                {
                    DailyGateAcc dailyGateAcc = _mapper.Map<DailyGateAcc>(info);
                    dailyGateAcc.ExpTotalAmt = 0;
                    dailyGateAcc.IncTotalAmt = 0;
                    dailyGateAcc.IncCreditAmt = 0;
                    dailyGateAcc.IncReceiveAmt = 0;
                    dailyGateAcc.CreatedDate = GetLocalStdDT();
                    dailyGateAcc.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                    _context.DailyGateAcc.Add(dailyGateAcc);
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

        public async Task<ResponseMessage> UpdateDailyGateAcc(DailyGateAccDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@code AND Cast(AccDate as Date) =@date", new SqlParameter("@code", info.GateCode), new SqlParameter("@date", info.AccDate)).SingleOrDefaultAsync();
                if (dailyGateAcc == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                   
                    dailyGateAcc.Remark = info.Remark;
                    dailyGateAcc.UpdatedDate = GetLocalStdDT();
                    dailyGateAcc.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> DeleteDailyGateAcc(string gate, DateTime date)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                DailyGateAcc dailyGateAcc = await _context.DailyGateAcc.FromSqlRaw("SELECT * FROM DailyGateAcc WHERE GateCode=@id AND Cast(AccDate as date)=@date", new SqlParameter("@id", gate), new SqlParameter("@date", date)).SingleOrDefaultAsync();

                if (dailyGateAcc == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                string code = gate + "-" + date.ToString("ddMMyy");

                //Remove IncomeList
                List<DailyGateIncome> incomeList = await _context.DailyGateIncome.FromSqlRaw("SELECT * FROM DailyGateIncome WHERE IncNo LIKE '" + code + "%' ").ToListAsync();
                if(incomeList.Count > 0)
                _context.DailyGateIncome.RemoveRange(incomeList);

                //Remove ExpenseList
                List<DailyGateExpense> expenseList = await _context.DailyGateExpense.FromSqlRaw("SELECT * FROM DailyGateExpense WHERE ExpNo LIKE '" + code + "%' ").ToListAsync();
                if (expenseList.Count > 0)
                    _context.DailyGateExpense.RemoveRange(expenseList);

                _context.DailyGateAcc.Remove(dailyGateAcc);
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



        #region StockMain 9_Dec_2024

        public async Task<DataTable> GetStockMainList()
        {
            string sql = "SELECT * FROM StockMain";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetStockMainById(string id)
        {
            string sql = "SELECT * FROM StockMain WHERE StockCode=@code";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@code", id));
            return dt;
        }

        public async Task<ResponseMessage> CreateStockMain(StockMainDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                StockMain data = await _context.StockMain.FromSqlRaw("SELECT Top 1 * FROM StockMain ORDER BY StockCode DESC").SingleOrDefaultAsync();
                if (data != null)
                {
                    int num = Convert.ToInt32(data.StockCode.Substring(1).ToString());
                    num++;
                    info.StockCode = "S" + num.ToString("d2");
                }
                else
                {
                    info.StockCode = "S" + "01";
                }
                StockMain stock = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode!=@code  AND REPLACE(StockName,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@code", info.StockCode), new SqlParameter("@name", info.StockName)).SingleOrDefaultAsync();

                if (stock != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    StockMain stockMain = _mapper.Map<StockMain>(info);
                    stockMain.Balance = 0;
                    stockMain.LastPrice = 0;
                    stockMain.CreatedDate = GetLocalStdDT();
                    stockMain.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                    _context.StockMain.Add(stockMain);
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

        public async Task<ResponseMessage> UpdateStockMain(StockMainDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                StockMain stock = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode=@code ", new SqlParameter("@code", info.StockCode)).SingleOrDefaultAsync();
                if (stock == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {
                    StockMain smain = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode!=@code  AND REPLACE(StockName,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@code", info.StockCode), new SqlParameter("@name", info.StockName)).SingleOrDefaultAsync();

                    if (smain != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Name duplicate!";
                    }
                    else
                    {
                        stock.StockName = info.StockName;
                        stock.UpdatedDate = GetLocalStdDT();
                        stock.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully updated!";
                    }
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        public async Task<ResponseMessage> DeleteStockMain(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                StockMain stock = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode=@id ", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (stock == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }


                //Remove stockhistory
                List<StockHistory> stockHistoryList = await _context.StockHistory.FromSqlRaw("SELECT * FROM StockHistory WHERE StockCode=@code", new SqlParameter("@code", id)).ToListAsync();
                if (stockHistoryList.Count > 0)
                    _context.StockHistory.RemoveRange(stockHistoryList);


                _context.StockMain.Remove(stock);
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


        #region StockHistory 9_Dec_2024


        public async Task<DataTable> GetStockHistoryList(string id, string stockType, string busNo)
        {
            string sql = "SELECT * FROM StockHistory Where StockCode=@code ";

            sql += stockType != "All" ? " AND StockType=@type" : "";

            sql += busNo != "All" ? " AND BusNo=@busNo" : "";

            sql += " Order By StockDate DESC";

            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@code", id), new SqlParameter("@type", stockType), new SqlParameter("@busNo", busNo));

            return dt;
        }

        public async Task<ResponseMessage> CreateStockHistory(StockHistoryDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                string code = Convert.ToDateTime(info.StockDate).ToString("ddMMyy") + "-";
                StockHistory stockHistory = await _context.StockHistory.FromSqlRaw("SELECT Top 1 * FROM StockHistory WHERE RegNo LIKE '" + code + "%' ORDER BY RegNo DESC").SingleOrDefaultAsync();
                if (stockHistory != null)
                {
                    int num = Convert.ToInt32(stockHistory.RegNo[^2..]);
                    num++;
                    info.RegNo = code + num.ToString("D2");
                }
                else
                {
                    info.RegNo = code + "01";
                }


                #region Update StockMain

                StockMain stockMain = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode=@code", new SqlParameter("@code", info.StockCode)).SingleOrDefaultAsync();

                if (info.StockType == "Receive")
                {
                    stockMain.Balance += info.Qty;
                    stockMain.LastPrice = info.LastPrice;
                }

                if (info.StockType == "Issue")
                    stockMain.Balance -= info.Qty;

                stockMain.UpdatedDate = GetLocalStdDT();
                stockMain.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;

                #endregion


                StockHistory stHistory = _mapper.Map<StockHistory>(info);
                stHistory.LastPrice = 0;
                stHistory.TotalAmt = 0;
                stHistory.IsCancel = false;
                if (info.LastPrice != null)
                {
                    stHistory.LastPrice = info.LastPrice;
                    stHistory.TotalAmt = info.Qty * info.LastPrice;
                }
                stHistory.CreatedDate = GetLocalStdDT();
                stHistory.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.StockHistory.Add(stHistory);
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

        public async Task<ResponseMessage> UpdateStockHistory(StockHistoryDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                StockHistory sthistory = await _context.StockHistory.FromSqlRaw("SELECT * FROM StockHistory WHERE RegNo=@code", new SqlParameter("@code", info.RegNo)).SingleOrDefaultAsync();
                if (sthistory == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {

                    #region Update StockMain

                    StockMain stockMain = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode=@code", new SqlParameter("@code", info.StockCode)).SingleOrDefaultAsync();

                    if (info.StockType == "Receive")
                    {
                        if (sthistory.StockType == "Receive")
                        {
                            stockMain.Balance -= sthistory.Qty;
                            stockMain.Balance += info.Qty;
                            stockMain.LastPrice = info.LastPrice;
                        }
                        else 
                        {
                            stockMain.Balance += info.Qty;
                          
                        }
                    }
                    else if (info.StockType == "Issue")
                    {
                        if (sthistory.StockType == "Issue")
                        {
                            stockMain.Balance -= sthistory.Qty;
                            stockMain.Balance -= info.Qty;
                        }
                        else
                        {
                            stockMain.Balance += sthistory.Qty;
                            stockMain.Balance += info.Qty;
                            stockMain.LastPrice = info.LastPrice;

                        }
                    }

                    #endregion

                    sthistory.StockType = info.StockType;
                    sthistory.BusNo = info.BusNo;
                    sthistory.Qty = info.Qty;
                    if (info.LastPrice != null)
                    {
                        sthistory.LastPrice = info.LastPrice;
                        sthistory.TotalAmt = sthistory.Qty * sthistory.LastPrice;
                    }
                    sthistory.Remark = info.Remark;
                    sthistory.UpdatedDate = GetLocalStdDT();
                    sthistory.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> CancelStockHistory(string id)
        {
            var msg = new ResponseMessage { Status = false };
            try
            {
                StockHistory stock = await _context.StockHistory.FromSqlRaw("SELECT * FROM StockHistory WHERE RegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();

                if (stock == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }

                #region Update StockMain

                StockMain smain = await _context.StockMain.FromSqlRaw("SELECT * FROM StockMain WHERE StockCode=@code", new SqlParameter("@code", stock.StockCode)).SingleOrDefaultAsync();

                if(smain != null)
                {
                    if (stock.StockType == "Receive") smain.Balance -= stock.Qty;

                    if (stock.StockType == "Issue") smain.Balance += stock.Qty;

                    smain.UpdatedDate = GetLocalStdDT();
                    smain.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                }

                #endregion

                stock.IsCancel = true;

                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Canceled successfully.";
            }

            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        #endregion



        #region Bus Expense 10-Dec-2024

        public async Task<DataTable> GetBusExpenseList()
        {
            string sql = "SELECT bus.*, ExpName FROM BusExpense bus Inner Join ExpenseType e ON bus.ExpCode = e.ExpCode";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<ResponseMessage> CreateBusExpense(BusExpenseDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };

            try
            {
                string code = Convert.ToDateTime(info.ExpDate).ToString("ddMMyy") + "-";
                BusExpense busExpense = await _context.BusExpense.FromSqlRaw("SELECT Top 1 * FROM BusExpense WHERE ExpNo LIKE '" + code + "%' ORDER BY ExpNo DESC").SingleOrDefaultAsync();
                if (busExpense != null)
                {
                    int num = Convert.ToInt32(busExpense.ExpNo[^3..]);
                    num++;
                    info.ExpNo = code + num.ToString("D3");
                }
                else
                {
                    info.ExpNo = code + "001";
                }

                BusExpense busExp = _mapper.Map<BusExpense>(info);
                busExp.TotalAmt = info.Price * info.Qty;
                busExp.CreatedDate = GetLocalStdDT();
                busExp.CreatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.BusExpense.Add(busExp);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully added";
                
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }


        public async Task<ResponseMessage> UpdateBusExpense(BusExpenseDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                BusExpense busExpense = await _context.BusExpense.FromSqlRaw("SELECT * FROM BusExpense WHERE ExpNo=@id ", new SqlParameter("@id", info.ExpNo)).SingleOrDefaultAsync();

                if (busExpense == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                    return msg;
                }
                else
                {
                    busExpense.ExpCode = info.ExpCode;
                    busExpense.BusNo = info.BusNo;
                    busExpense.Qty = info.Qty;
                    busExpense.Price = info.Price;
                    busExpense.TotalAmt = busExpense.Qty * busExpense.Price;
                    busExpense.Remark = info.Remark;
                    busExpense.UpdatedDate = GetLocalStdDT();
                    busExpense.UpdatedUser = _httpContextAccessor.HttpContext.User.Identity.Name;
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

        public async Task<ResponseMessage> DeleteBusExpense(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                BusExpense expense = await _context.BusExpense.FromSqlRaw("SELECT * FROM BusExpense WHERE ExpNo=@id", new SqlParameter("@id", id)).FirstOrDefaultAsync();

                if (expense == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.BusExpense.Remove(expense);
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
