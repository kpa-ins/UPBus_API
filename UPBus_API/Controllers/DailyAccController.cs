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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyGateExpense(string id)
        {
            ResponseMessage msg = await _service.DeleteDailyGateExpense(id);
            return Ok(msg);
        }

        #endregion



        #region Daily Gate Expense 5-Dec-2024

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyGateIncome(string id)
        {
            ResponseMessage msg = await _service.DeleteDailyGateIncome(id);
            return Ok(msg);
        }

        #endregion
    }
}
