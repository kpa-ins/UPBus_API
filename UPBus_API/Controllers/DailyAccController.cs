using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UPBus_API.DTOs;
using UPBus_API.Services;

namespace UPBus_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DailyAccController(DailyAccService service) : ControllerBase
    {
        private readonly DailyAccService _service = service;


        #region Daily Gate Expense 5-Dec-2024

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyGateExpenseList(string id)
        {
            DataTable dt = await _service.GetDailyGateExpenseList(id);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyGateExpense(DailyGateExpenseDto info)
        {
            ResponseMessage msg = await _service.CreateDailyGateExpense(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDailyGateExpense(DailyGateExpenseDto info)
        {
            ResponseMessage msg = await _service.UpdateDailyGateExpense(info);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDailyGateExpense(string id, string date)
        {
            ResponseMessage msg = await _service.DeleteDailyGateExpense(id, date);
            return Ok(msg);
        }

        #endregion



        #region Daily Gate Income 5-Dec-2024

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyGateIncomeList(string id)
        {
            DataTable dt = await _service.GetDailyGateIncomeList(id);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyGateIncome(DailyGateIncomeDto info)
        {
            ResponseMessage msg = await _service.CreateDailyGateIncome(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDailyGateIncome(DailyGateIncomeDto info)
        {
            ResponseMessage msg = await _service.UpdateDailyGateIncome(info);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDailyGateIncome(string id, string date)
        {
            ResponseMessage msg = await _service.DeleteDailyGateIncome(id, date);
            return Ok(msg);
        }

        #endregion



        #region Daily Gate Acc 6-Dec-2024

        [HttpGet]
        public async Task<IActionResult> GetDailyGateAccList()
        {
            DataTable dt = await _service.GetDailyGateAccList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetDailyGateAccById(string gate, string date)
        {
            DataTable dt = await _service.GetDailyGateAccById(gate, date);
            return Ok(dt);
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateDailyGateAcc(DailyGateAccDto info)
        {
            ResponseMessage msg = await _service.CreateDailyGateAcc(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDailyGateAcc(DailyGateAccDto info)
        {
            ResponseMessage msg = await _service.UpdateDailyGateAcc(info);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDailyGateAcc(string gate, DateTime date)
        {
            ResponseMessage msg = await _service.DeleteDailyGateAcc(gate, date);
            return Ok(msg);
        }

        #endregion


        #region Stock Main 9-Dec-2024

        [HttpGet]
        public async Task<IActionResult> GetStockMainList()
        {
            DataTable dt = await _service.GetStockMainList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetStockMainById(string id)
        {
            DataTable dt = await _service.GetStockMainById(id);
            return Ok(dt);
        }



        [HttpPost]
        public async Task<IActionResult> CreateStockMain(StockMainDto info)
        {
            ResponseMessage msg = await _service.CreateStockMain(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStockMain(StockMainDto info)
        {
            ResponseMessage msg = await _service.UpdateStockMain(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockMain(string id)
        {
            ResponseMessage msg = await _service.DeleteStockMain(id);
            return Ok(msg);
        }

        #endregion


        #region Stock History 9-Dec-2024

        [HttpGet]
        public async Task<IActionResult> GetStockHistoryList(string id, string stockType, string busNo)
        {
            DataTable dt = await _service.GetStockHistoryList(id, stockType, busNo);
            return Ok(dt);
        }

         [HttpPost]
        public async Task<IActionResult> CreateStockHistory(StockHistoryDto info)
        {
            ResponseMessage msg = await _service.CreateStockHistory(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStockHistory(StockHistoryDto info)
        {
            ResponseMessage msg = await _service.UpdateStockHistory(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelStockHistory(string id)
        {
            ResponseMessage msg = await _service.CancelStockHistory(id);
            return Ok(msg);
        }

        #endregion



        #region Bus Expense 10-Dec-2024

        [HttpGet]
        public async Task<IActionResult> GetBusExpenseList()
        {
            DataTable dt = await _service.GetBusExpenseList();
            return Ok(dt);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBusExpense(BusExpenseDto info)
        {
            ResponseMessage msg = await _service.CreateBusExpense(info);
            return Ok(msg);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBusExpense(BusExpenseDto info)
        {
            ResponseMessage msg = await _service.UpdateBusExpense(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusExpense(string id)
        {
            var msg = await _service.DeleteBusExpense(id);
            return Ok(msg);
        }

        #endregion
    }
}
