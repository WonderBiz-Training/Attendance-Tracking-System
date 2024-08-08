using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.DTO
{
    public class AttendanceLogDto
    {
    }

    public record CreateAttendanceLogDto(
        [Required(ErrorMessage = "User Id is required")] long UserId,
        DateTime AttendanceLogTime,
        [Required(ErrorMessage = "Please Specify Check Type of Attendance Log")] 
        string CheckType
        );

    public record UpdateAttendanceLogDto(
        long UserId,
        DateTime AttendanceLogTime,
        string CheckType
        );

    public record GetAttendanceLogDto(
        long Id,
        long UserId,
        DateTime AttendanceLogTime,
        string CheckType
        );

    public record GetAttendanceLogSummaryDto(int Total, int Present, int Wfh, int Absent);

    public record GetAttendanceReportDto(
        string FirstName, 
        string LastName, 
        string Status, 
        DateTime InTime
        );

    public record GetActivityRecordDto(
        DateTime InTime,
        DateTime OutTime,
        TimeSpan InHours
    );

    public record GetTotalHours(
        string ProfilePic,
        string FirstName,
        string LastName,
        TimeSpan TotalHours
    );
}
