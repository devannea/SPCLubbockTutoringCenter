using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public class TimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserService _userService;

        public TimesheetService(ITimesheetRepository timesheetRepository, IProfileRepository profileRepository, IUserService userService)
        {
            _timesheetRepository = timesheetRepository;
            _profileRepository = profileRepository;
            _userService = userService;
        }

        public Timesheet Add(Timesheet newTimesheet)
        {
            var profileId = newTimesheet.ProfileId;
            var profile = _profileRepository.Get(profileId);
            var profileUserId = profile.UserId;
            var loggedInUserId = _userService.CurrentUserId;
            if (loggedInUserId != profileUserId)
            {
                throw new Exception("User cannot add a timesheet to this profile.");
            }
            newTimesheet.TimeIn = DateTime.Now;
            newTimesheet.TimeOut = DateTime.Now;
            return _timesheetRepository.Add(newTimesheet);
        }

        public Timesheet Get(int id)
        {
            return _timesheetRepository.Get(id);
        }

        public IEnumerable<Timesheet> GetAll()
        {
            return _timesheetRepository.GetAll();
        }

        public IEnumerable<Timesheet> GetProfileTimesheets(int profileId)
        {
            return _timesheetRepository.GetProfileTimesheets(profileId);
        }

        public Timesheet Update(Timesheet updatedTimesheet)
        {
            return _timesheetRepository.Update(updatedTimesheet);
        }

        public void Remove(int id)
        {
            var timesheet = this.Get(id);
            _timesheetRepository.Remove(id);
        }
    }
}
