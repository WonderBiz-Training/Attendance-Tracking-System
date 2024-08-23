using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IAttendanceLogServices
    {
        Task<IEnumerable<GetAttendanceLogsWithDetailsDto>> GetAllAttendanceLogsAsync(int? count, DateTime? startDate);

        Task<GetAttendanceLogDto> GetAttendanceLogAsync(long id);

        Task<IEnumerable<GetAttendanceLogDto>> GetAttendanceLogByUserId(long userId);

        Task<GetAttendanceLogSummaryDto> GetAttendanceLogSummary(DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<GetInActivityRecordDto>> GetInActivityRecord(long? userId, DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<GetSumTotalHoursDto>> GetTotalInActivity(long? userId, DateTime? startDate, DateTime? endDate, string? reportType);

        Task<IEnumerable<GetOutActivityRecordDto>> GetOutActivityRecord(long? userId, DateTime? startDate, DateTime? endDate);

        Task<IEnumerable<GetSumTotalHoursDto>> GetTotalOutActivity(long? userId, DateTime? startDate, DateTime? endDate, string? reportType);

        Task<IEnumerable<GetStatusOfAttendanceLogDto>> GetStatusOfAttendanceLog(string? FirstName, DateTime? Date);

        Task<IEnumerable<GetAttendanceLogsWithDetailsDto>> GetCurrentStatusOfAttendanceLog(string type, DateTime? date, int? count);

        Task<GetAttendanceLogDto> CreateAttendanceLogAsync(CreateAttendanceLogDto attendanceLogDto);

        Task<GetAttendanceLogDto> UpdateAttendanceLogAsync(long id, UpdateAttendanceLogDto attendanceLogDto);

        Task<bool> DeleteAttendanceLogAsync(long id);

        Task<IEnumerable<GetTotalHoursDto>> GetTotalHoursOfEmployee(DateTime? startDate, DateTime? endDate, string? reportType);

        Task<IEnumerable<GetAttendanceLogsWithDetailsDto>> CreateMultipleAttendanceLogsAsync(IEnumerable<CreateAttendanceLogDto> attendanceLogsDto);

    }
}
