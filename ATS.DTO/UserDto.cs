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
        long ContactNo,
        long CreatedBy
        );

    public record UpdateUserDto(
        [MaxLength(50)] string Email,
        [MaxLength(50)] string Password,
        long ContactNo,
        bool IsActive,
        long UpdatedBy
        );

    public record GetUserDto(
        long Id,
        string Email,
        string Password,
        long ContactNo,
        bool isActive
        );
}
