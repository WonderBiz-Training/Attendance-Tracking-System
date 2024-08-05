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
    [Table("Genders")]
    [Index(nameof(GenderName), IsUnique = true)]
    [Index(nameof(GenderCode), IsUnique = true)]
    public class Gender : BaseEntity
    {
        [Required(ErrorMessage = "Gender Name is required"), MaxLength(50)]
        public string GenderName { get; set; } = string.Empty;

        [MaxLength(10)]
        public string GenderCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
