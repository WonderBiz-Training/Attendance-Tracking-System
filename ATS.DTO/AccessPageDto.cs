using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class AccessPageDto
    {
    }

    public record CreateAccessPageDto(
        long RoleId,
        long PageId,
        long CreatedBy
        );

    public record UpdateAccessPageDto(
        long? RoleId,
        long? PageId,
        bool? IsActive,
        long? UpdatedBy
        );

    public record GetAccessPageDto(
        long Id,
        long RoleId,
        long PageId,
        bool isActive
        );
}
