using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCLTCAPI.ApiModels
{
    public class TimesheetModel
    {
        public int Id { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int ProfileId { get; set; }
        public string ProfileName { get; set; }
    }
}
