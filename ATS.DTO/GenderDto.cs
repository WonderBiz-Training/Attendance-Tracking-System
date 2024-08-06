using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class GenderDto
    {
    }

    public record CreateGenderDto(
        [Required(ErrorMessage = "Gender Name is required"), MaxLength(50)] string GenderName,
        long CreatedBy,
        [MaxLength(10)] string GenderCode = ""
        );

    public record UpdateGenderDto(
        [MaxLength(50)] string GenderName,
        bool IsActive,
        long UpdatedBy,
        [MaxLength(10)] string GenderCode = ""
        );

    public record GetGenderDto(
        long Id,
        string GenderName,
        string GenderCode,
        bool IsActive
    );
}
