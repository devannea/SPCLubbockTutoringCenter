using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCLCTAPI.Core.Services;
using SPCLTCAPI.ApiModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPCLTCAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TimesheetsController : ControllerBase
    {

        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet("/api/profiles/{profileId}/timesheets")]
        [AllowAnonymous]
        public IActionResult Get(int profileId)
        {
            var timesheets = _timesheetService.GetProfileTimesheets(profileId);
            var timesheetModels = timesheets.Select(t => t.ToApiModel());
            return Ok(timesheetModels);
        }

        // GET api/profiles/{profileId}/timesheets/{timesheetId}
        [HttpGet("/api/profiles/{profileId}/timesheets/{timesheetId}")]
        [AllowAnonymous]
        public IActionResult Get(int profileId, int timesheetId)
        {
            var timesheets = _timesheetService.GetProfileTimesheets(profileId);
            var timesheet = timesheets.FirstOrDefault(t => t.Id == timesheetId);
            var timesheetModel = timesheet.ToApiModel();
            return Ok(timesheetModel);
        }

        // POST /api/profiles/{profileId}/timesheet
        [HttpPost("/api/profiles/{profileId}/timesheets")]
        public IActionResult Post(int profileId, [FromBody]TimesheetModel timesheetModel)
        {
            try
            {
                var timesheet = timesheetModel.ToDomainModel();
                _timesheetService.Add(timesheet);
                return Ok(timesheet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddTimesheet", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT /api/profiles/{profileId}/timesheets/{timesheetId}
        [HttpPut("/api/profiles/{profileId}/timesheets/{timesheetId}")]
        public IActionResult Put(int profileId, int timesheeId, [FromBody]TimesheetModel timesheetModel)
        {
            try
            {
                var updatedTimesheet = _timesheetService.Update(timesheetModel.ToDomainModel());
                return Ok(updatedTimesheet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateTimesheet", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE /api/profiles/{profileId}/timesheets/{timesheetId}
        [HttpDelete("/api/profiles/{profileId}/timesheets/{timesheetId}")]
        public IActionResult Delete(int profileId, int timesheetId)
        {
            try
            {
                _timesheetService.Remove(timesheetId);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeleteTimesheet", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE /api/timesheets
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete()
        {
            // code to delete all timesheets goes here...

            return Ok("Deleted all timesheets");
        }
    }
}
