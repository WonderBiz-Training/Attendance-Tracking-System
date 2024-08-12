using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    public class GetTotalOutHours
    {
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public Double TotalOutHours { get; set; }
    }
}
