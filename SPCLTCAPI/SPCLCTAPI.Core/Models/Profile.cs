using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Models
{
    public class Profile : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Timesheet> Timesheets { get; set; }
    }
}
