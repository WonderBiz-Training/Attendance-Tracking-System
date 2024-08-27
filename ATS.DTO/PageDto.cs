using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class PageDto
    {

    }

    public record CreatePageDto(
        [MaxLength(100)] string PageCode,
        [Required(ErrorMessage = "Page Title is Required"), MaxLength(100)] string PageTitle,
        long CreatedBy
    );

    public record UpdatePageDto(
        string PageCode,
        string PageTitle,
        long UpdatedBy
    );

    public record GetPageDto(
        long Id,
        string PageCode,
        string PageTitle
    );
}
