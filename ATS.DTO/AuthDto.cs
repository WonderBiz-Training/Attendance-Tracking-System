using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class AuthDto
    {

    }

    public record SignUpDto(
       [Required(ErrorMessage = "First Name is required"), MaxLength(50)] string FirstName,
       [Required(ErrorMessage = "Last Name is required"), MaxLength(50)] string LastName,
       [Required(ErrorMessage = "Email is required"), MaxLength(50), EmailAddress] string Email,
       [Required(ErrorMessage = "Contact Number is required")] string ContactNo,
       [Required(ErrorMessage = "Password is required"), MaxLength(50)] string Password,
       [Required(ErrorMessage = "Profile Pic is required")] string ProfilePic,
       long? RoleId,
       string? EmployeeCode
    );

    public record LogInDto(
       
       [Required(ErrorMessage = "Email is required"), MaxLength(50), EmailAddress] string Email,
       [Required(ErrorMessage = "Password is required"), MaxLength(50)] string Password
    );

    public record GetLogInDto(
       long Id,
       string Email,
       string Password,
       long RoleId,
       IEnumerable<GetAccessPageDto> PageList
    );

    public record GetSignUpDto(
       long UserId,
       string FirstName,
       string LastName,
       string Email,
       string ContactNo,
       string Password,
       string ProfilePic,
       long? RoleId
    );


}
