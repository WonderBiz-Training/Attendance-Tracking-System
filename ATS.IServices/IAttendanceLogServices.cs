using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IAttendanceLogServices
    {
        Task<IEnumerable<GetAttendanceLogDto>> GetAllAttendanceLogsAsync();

        Task<GetAttendanceLogDto> GetAttendanceLogAsync(long id);

        Task<IEnumerable<GetAttendanceLogDto>> GetAttendanceLogByUserId(long userId);

        Task<GetAttendanceLogSummaryDto> GetAttendanceLogSummary(DateTime? currentDate = default);

        Task<GetAttendanceLogDto> CreateAttendanceLogAsync(CreateAttendanceLogDto genderDto);

        Task<GetAttendanceLogDto> UpdateAttendanceLogAsync(long id, UpdateAttendanceLogDto genderDto);

        Task<bool> DeleteAttendanceLogAsync(long id);
    }
}
