using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public interface ITimesheetRepository
    {
        Timesheet Add(Timesheet Timesheet);
        Timesheet Update(Timesheet Timesheet);
        Timesheet Get(int id);
        IEnumerable<Timesheet> GetAll();
        void Remove(int id);
        IEnumerable<Timesheet> GetProfileTimesheets(int profileId);
    }
}
