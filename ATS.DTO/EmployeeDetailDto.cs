using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class EmployeeDetailDto
    {
    }

    public record CreateEmployeeDetailDto(
        [Required(ErrorMessage = "User Id is Required")] long UserId,
        [Required(ErrorMessage = "Gender Id is Required")] long GenderId,
        [Required(ErrorMessage = "Designation Id is Required")] long DesignationId,
        string EmployeeId,
        [Required(ErrorMessage = "First Name is required"), MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        [Required(ErrorMessage = "Profile Pic is Required")] string ProfilePic,
        long CreatedBy
    );

    public record UpdateEmployeeDetailDto(
        [Required(ErrorMessage = "User Id is Required")] long UserId,
        [Required(ErrorMessage = "Gender Id is Required")] long GenderId,
        [Required(ErrorMessage = "Designation Id is Required")] long DesignationId,
        string EmployeeId,
        [Required(ErrorMessage = "First Name is required"), MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        [Required(ErrorMessage = "Profile Pic is Required")] string ProfilePic,
        bool IsActive,
        long UpdatedBy
    );

    public record GetEmployeeDetailDto(
        long Id,
        string Email,
        string GenderName,
        string DesignationName,
        string EmployeeId,
        [MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        string ProfilePic
    );

    public record GetSearchDto(
        int Count,
        IEnumerable<GetEmployeeDetailDto> Employees
    );
}
