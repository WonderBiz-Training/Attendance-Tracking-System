/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    [Table("Logs")]
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public User User { get; set; }
        [Required(ErrorMessage = "User Id is required")]
        public long UserId { get; set; }

        public DateTime Time { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
*/