using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    [Table("AttendanceLogs")]
    public class AttendanceLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual User User { get; set; }
        [ForeignKey("Users"), Required(ErrorMessage = "User Id is required")]
        public long UserId { get; set; }

        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Please Specify Check Type of Log")]
        public string CheckType { get; set; }
    }
}
