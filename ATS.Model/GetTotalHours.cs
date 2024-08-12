using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    public class GetTotalHours
    {
        public int UserId { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime LogDate { get; set; }
        public DateTime LastCheckInTime { get; set; }
        public DateTime LastCheckoutTime { get; set; }
        public string TotalTimeSpanFormatted { get; set; } = string.Empty;
    }
}
