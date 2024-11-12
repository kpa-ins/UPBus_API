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

        #region Gate 9-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetGateList()
        {
            DataTable dt = await _service.GetGateList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGate(GateDto info)
        {
            ResponseMessage msg = await _service.CreateGate(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGate(GateDto info)
        {
            ResponseMessage msg = await _service.UpdateGate(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGate(string id)
        {
            ResponseMessage msg = await _service.DeleteGate(id);
            return Ok(msg);
        }

        #endregion

        #region Expense Type 11-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetExpenseTypeList()
        {
            DataTable dt = await _service.GetExpenseTypeList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveExpenseType(ExpenseTypeDto info)
        {
            ResponseMessage msg = await _service.SaveExpenseType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpenseType(ExpenseTypeDto info)
        {
            ResponseMessage msg = await _service.UpdateExpenseType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType(string id)
        {
            var msg = await _service.DeleteExpenseType(id);
            return Ok(msg);
        }


        #endregion

        #region Income Type 11-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetIncomeTypeList()
        {
            DataTable dt = await _service.GetIncomeTypeList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveIncomeType(IncomeTypeDto info)
        {
            ResponseMessage msg = await _service.SaveIncomeType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIncomeType(IncomeTypeDto info)
        {
            ResponseMessage msg = await _service.UpdateIncomeType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeType(string id)
        {
            var msg = await _service.DeleteIncomeType(id);
            return Ok(msg);
        }


        #endregion

        #region Trip Type 11-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetTripTypeList()
        {
            DataTable dt = await _service.GetTripTypeList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTripType(TripTypeDto info)
        {
            ResponseMessage msg = await _service.SaveTripType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTripType(TripTypeDto info)
        {
            ResponseMessage msg = await _service.UpdateTripType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripType(string id)
        {
            var msg = await _service.DeleteTripType(id);
            return Ok(msg);
        }


        #endregion
    }
}
