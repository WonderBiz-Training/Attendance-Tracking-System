using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    [Table("Users")]
    [Index(nameof(Email),IsUnique = true)]

    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Email is Required"), MaxLength(50) , EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is Required")]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        public long ContactNo { get; set; }
    }
}
