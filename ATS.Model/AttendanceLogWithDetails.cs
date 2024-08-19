using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    public class AttendanceLogWithDetails
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AttendanceLogTime { get; set; }
        public string CheckType { get; set; }
    }
}
