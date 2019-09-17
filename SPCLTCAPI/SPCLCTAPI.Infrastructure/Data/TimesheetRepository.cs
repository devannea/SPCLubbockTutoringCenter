using Microsoft.EntityFrameworkCore;
using SPCLCTAPI.Core.Models;
using SPCLCTAPI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPCLCTAPI.Infrastructure.Data
{
    class TimesheetRepository : ITimesheetRepository
    {
        private readonly AppDbContext _dbContext;
        public TimesheetRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Timesheet Add(Timesheet Timesheet)
        {
            _dbContext.Timesheets.Add(Timesheet);
            _dbContext.SaveChanges();
            return Timesheet;
        }

        public Timesheet Get(int id)
        {
            return _dbContext.Timesheets
               .Include(t => t.Profile)
               .Include(t => t.Profile.User)
               .SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Timesheet> GetProfileTimesheets(int profileId)
        {
            return _dbContext.Timesheets
               .Include(t => t.Profile)
               .Include(t => t.Profile.User)
               .ToList();
        }

        public IEnumerable<Timesheet> GetAll()
        {
            return _dbContext.Timesheets
                .Include(t => t.Profile)
                .Include(t => t.Profile.User)
                .ToList();
        }

        public Timesheet Update(Timesheet updatedTimesheet)
        {
            var currentTimesheet = _dbContext.Timesheets.Find(updatedTimesheet.Id);
            if (currentTimesheet == null) return null;
            _dbContext.Entry(currentTimesheet)
                .CurrentValues
                .SetValues(updatedTimesheet);
            _dbContext.Timesheets.Update(currentTimesheet);
            _dbContext.SaveChanges();
            return currentTimesheet;
        }

        public void Remove(int id)
        {
            var deletedTimesheet = Get(id);
            _dbContext.Timesheets.Remove(deletedTimesheet);
            _dbContext.SaveChanges();
        }
    }
}
