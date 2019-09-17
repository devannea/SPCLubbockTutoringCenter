using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCLTCAPI.ApiModels
{
    public static class TimesheetMappingExtensions
    {
        public static TimesheetModel ToApiModel(this Timesheet timesheet)
        {
            return new TimesheetModel
            {
                Id = timesheet.Id,
                TimeIn = timesheet.TimeIn,
                TimeOut = timesheet.TimeOut,
                ProfileId = timesheet.ProfileId,
                ProfileName = timesheet.Profile.Name
            };
        }

        public static Timesheet ToDomainModel(this TimesheetModel timesheetModel)
        {
            return new Timesheet
            {
                Id = timesheetModel.Id,
                TimeIn = timesheetModel.TimeIn,
                TimeOut = timesheetModel.TimeOut,
                ProfileId = timesheetModel.ProfileId
            };
        }

        public static IEnumerable<TimesheetModel> ToApiModels(this IEnumerable<Timesheet> Users)
        {
            return Users.Select(u => u.ToApiModel());
        }

        public static IEnumerable<Timesheet> ToDomainModels(this IEnumerable<TimesheetModel> UserModels)
        {
            return UserModels.Select(u => u.ToDomainModel());
        }
    }
}
