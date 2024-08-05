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
    [Table("Designations")]
    [Index(nameof(DesignationName), IsUnique = true)]
    [Index(nameof(DesignationCode), IsUnique = true)]
    public class Designation
    {
        [Required(ErrorMessage = "Designation Name is Required!")]
        [StringLength(100)]
        public string DesignationName { get; set; } = string.Empty;
        public string DesignationCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
