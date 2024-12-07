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

        [HttpGet]
        public async Task<IActionResult> GetDailyGateExpenseList()
        {
            DataTable dt = await _service.GetDailyGateExpenseList();
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

        [HttpGet]
        public async Task<IActionResult> GetDailyGateIncomeList()
        {
            DataTable dt = await _service.GetDailyGateIncomeList();
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
    }
}
