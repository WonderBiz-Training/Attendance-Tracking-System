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
    [Table("Roles")]
    [Index(nameof(RoleName), IsUnique = true)]
    public class Role : BaseEntity
    {
        [Required(ErrorMessage = "Role Name is required"), MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
