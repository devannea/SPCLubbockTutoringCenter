using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Models
{
    public class Timesheet : IEntity<int>
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
