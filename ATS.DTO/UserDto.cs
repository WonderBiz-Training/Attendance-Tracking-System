using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class UserDto
    {
    }

    public record CreateUserDto(
        [Required(ErrorMessage = "Email is required"), MaxLength(50), EmailAddress] string Email,
        [Required(ErrorMessage = "Password is required"), MaxLength (50)] string Password,
        string ContactNo,
        long CreatedBy
        );

    public record UpdateUserDto(
        [MaxLength(50)] string Email,
        [MaxLength(50)] string OldPassword,
        [MaxLength(50)] string NewPassword,
        string? ContactNo,
        bool? IsActive,
        long? UpdatedBy
        );

    public record GetUserDto(
        long Id,
        string Email,
        string Password,
        string ContactNo,
        bool isActive,
        long RoleId
        );
}
