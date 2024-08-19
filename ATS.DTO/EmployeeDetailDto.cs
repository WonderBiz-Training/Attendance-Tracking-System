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
        //long GenderId,
        //long DesignationId,
        string? EmployeeCode,
        [Required(ErrorMessage = "First Name is required"), MaxLength(50)] string FirstName,
        [Required(ErrorMessage = "Last Name is required"), MaxLength(50)] string LastName,
        [Required(ErrorMessage = "Profile Pic is Required")] string ProfilePic,
        long CreatedBy
    );

    public record UpdateEmployeeDetailDto(
        long UserId,
        long GenderId,
        long DesignationId,
        string? EmployeeCode,
        [MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        byte?[] FaceEncoding,
        string ProfilePic,
        bool IsActive,
        long UpdatedBy
    );

    public record GetEmployeeDetailDto(
        long Id,
        long UserId,
        string Email,
        string GenderName,
        string DesignationName,
        string EmployeeCode,
        [MaxLength(50)] string FirstName,
        [MaxLength(50)] string LastName,
        string ProfilePic,
        byte[] FaceEncoding
    );

    public record GetSearchDto(
        int Count,
        IEnumerable<GetEmployeeDetailDto> Employees
    );
}
