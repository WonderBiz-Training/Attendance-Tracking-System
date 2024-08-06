using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class DesignationDto
    {
        
    }

    public record CreateDesignationDto(
        [Required(ErrorMessage = "Designation Name is required")] string DesignationName,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        
        string DesignationCode = " "
        );

    public record UpdateDesignationDto(
        [Required(ErrorMessage = "Id is required")] long Id,
        [Required(ErrorMessage = "Designation Name is required")] string DesignationName,
        bool IsActive,
        DateTime UpdatedAt,
        string DesignationCode = " "
    );
    public record GetDesignationDto(
        long Id,
        string DesignationName,
        string DesignationCode,
        bool IsActive
    );
}
