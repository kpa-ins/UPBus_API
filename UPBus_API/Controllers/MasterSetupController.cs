﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetActiveGate()
        {
            DataTable dt = await _service.GetActiveGate();
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
        public async Task<IActionResult> GetExpenseTypeList(string expenseType)
        {
            DataTable dt = await _service.GetExpenseTypeList(expenseType);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetActiveExpenseType(string type)
        {
            DataTable dt = await _service.GetActiveExpenseType(type);
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
        public async Task<IActionResult> GetIncomeTypeList(string incomeType)
        {
            DataTable dt = await _service.GetIncomeTypeList(incomeType);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveIncomeType(string type)
        {
            DataTable dt = await _service.GetActiveIncomeType(type);
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

        #region Track Type 11-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetTrackTypeList()
        {
            DataTable dt = await _service.GetTrackTypeList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrackType(TrackTypeDto info)
        {
            ResponseMessage msg = await _service.SaveTrackType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrackType(TrackTypeDto info)
        {
            ResponseMessage msg = await _service.UpdateTrackType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackType(string id)
        {
            var msg = await _service.DeleteTrackType(id);
            return Ok(msg);
        }


        #endregion

        #region Daily Plan 23-Nov-2024

        [HttpGet]
        public async Task<IActionResult> GetDailyPlanList()
        {
            DataTable dt = await _service.GetDailyPlanList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrackTypes()
        {
            DataTable dt = await _service.GetTrackTypes();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetBusData()
        {
            DataTable dt = await _service.GetBusData();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetDriverByBusNo(string busNo)
        {
            if (string.IsNullOrEmpty(busNo))
            {
                return BadRequest(new { message = "Bus number is required" });
            }

            var driverName = await _service.GetDriverByBusNo(busNo);
            if (string.IsNullOrEmpty(driverName))
            {
                return NotFound(new { message = $"No driver found for bus number {busNo}" });
            }
            return Ok(new { DriverName = driverName });
        }


        [HttpPost]
        public async Task<IActionResult> SaveDailyPlan(DailyPlanDto info)
        {
            ResponseMessage msg = await _service.SaveDailyPlan(info);
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetDailyPlanId(string id)
        {
            DailyPlanDto dailyPlanDto = await _service.GetDailyPlanId(id);
            return Ok(dailyPlanDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDailyPlan(DailyPlanDto info)
        {
            ResponseMessage msg = await _service.UpdateDailyPlan(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyPlan(string id)
        {
            var msg = await _service.DeleteDailyPlan(id);
            return Ok(msg);
        }

        #endregion

        #region Gas Station 4-Dec-2024

        [HttpGet]
        public async Task<IActionResult> GetGasStationList()
        {
            DataTable dt = await _service.GetGasStationList();
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGasStation(GasStationDto info)
        {
            ResponseMessage msg = await _service.CreateGasStation(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGasStation(GasStationDto info)
        {
            ResponseMessage msg = await _service.UpdateGasStation(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasStation(string id)
        {
            ResponseMessage msg = await _service.DeleteGasStation(id);
            return Ok(msg);
        }

        #endregion

    }
}
