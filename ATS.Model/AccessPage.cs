using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    [Table("AccessPages")]
    public class AccessPage : BaseEntity
    {
        public virtual Role Role { get; set; }

        [ForeignKey("Roles"), Required(ErrorMessage = "Role ID is required")]
        public long RoleId { get; set; }

        public virtual Page Page { get; set; }

        [ForeignKey("Pages"), Required(ErrorMessage = "Page ID is required")]
        public long PageId { get; set; }
        public bool IsActive { get; set; } = false;

    }
}
