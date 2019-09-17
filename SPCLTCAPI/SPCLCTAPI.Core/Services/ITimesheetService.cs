using SPCLCTAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Services
{
    public interface ITimesheetService
    {
        Timesheet Add(Timesheet newPost);
        Timesheet Get(int id);
        IEnumerable<Timesheet> GetAll();
        IEnumerable<Timesheet> GetProfileTimesheets(int profileId);
        Timesheet Update(Timesheet updatedPost);
        void Remove(int id);
    }
}
