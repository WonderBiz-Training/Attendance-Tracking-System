using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    public class GetTotalHours
    {
        public long UserId { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public string TotalTimeSpanFormatted { get; set; }
    }
}
