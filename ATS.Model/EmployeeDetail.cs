using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    [Table("EmployeeDetails")]
    public class EmployeeDetail : BaseEntity
    {
        public virtual User User { get; set; }

        [ForeignKey("Users"), Required(ErrorMessage = "User ID is required")]
        public long UserId { get; set; }
        public string EmployeeId { get; set; } = string.Empty;

        [Required(ErrorMessage ="First Name is Required") ,MaxLength(50)]
        public string FirstName { get; set; } = string.Empty ;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        public virtual Designation Designation { get; set; }

        [ForeignKey("Designations"), Required(ErrorMessage = "Designation ID is required")]
        public long DesignationId { get; set; }
        public virtual Gender Gender { get; set; }

        [ForeignKey("Genders"), Required(ErrorMessage = "Gender ID is required")]
        public long GenderId { get; set; }

        [Required(ErrorMessage ="Profile Pic is Required")]
        public string ProfilePic { get; set; } = string.Empty;

        public byte[] FaceEncoding { get; set; }
    }
}
