using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Models
{
    public class Student : IEntity<int>
    {
        public int Id { get; set; }

        public int TimesheetId { get; set; }
        public Timesheet Timesheet { get; set; }

        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentFullName { get { return $"{StudentFirstName} {StudentLastName}"; } }
    }
}
