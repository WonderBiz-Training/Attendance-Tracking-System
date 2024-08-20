using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IRepository
{
    public interface IAttendanceLogRepository : IRepository<AttendanceLog>
    {
        Task<IEnumerable<AttendanceLog>> GetAttendanceLogByUserId(long userId);
        Task<IEnumerable<AttendanceLog>> GetSummaryReport(DateTime startDate, DateTime endDate, string check);
        Task<IEnumerable<AttendanceLog>> GetActivityReport(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AttendanceLog>> GetAttendanceReport(DateTime date);
        Task<IEnumerable<AttendanceLog>> GetAllAttendanceLogs(int? count, DateTime? startDate);
        Task<IEnumerable<GetStatusOfAttendanceLog>> GetAllStatusOfAttendanceLog(DateTime Date);
        Task<IEnumerable<GetStatusOfAttendanceLog>> GetPacificStatusOfAttendanceLog(string firstName);
        Task<IEnumerable<GetTotalInHours>> GetTotalInHours(long? userId, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<GetTotalOutHours>> GetTotalOutHours(long? userId, DateTime? startDate, DateTime? endDate);

    }
}
