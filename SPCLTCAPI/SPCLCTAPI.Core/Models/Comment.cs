using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Models
{
    public class Comment : IEntity<int>
    {
        public int Id { get; set; }

        public int TimesheetId { get; set; }
        public Timesheet Timesheet { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
