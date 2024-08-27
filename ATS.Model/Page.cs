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
    [Table("Pages")]
    [Index(nameof(PageCode),IsUnique = true)]
    public class Page : BaseEntity
    {
        [MaxLength(100)]
        public string PageCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Page Title is Required"), MaxLength(100)]
        public string PageTitle { get; set; } = string.Empty;
    }
}
