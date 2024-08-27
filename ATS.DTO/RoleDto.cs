using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class RoleDto
    {
    }

    public record CreateRoleDto(
        [Required(ErrorMessage = "Role Name is required"), MaxLength(50)] string RoleName,
        long CreatedBy
        );

    public record UpdateRoleDto(
        [MaxLength(50)] string RoleName,
        bool IsActive,
        long UpdatedBy
        );

    public record GetRoleDto(
        long Id,
        string RoleName,
        bool IsActive
    );
}
