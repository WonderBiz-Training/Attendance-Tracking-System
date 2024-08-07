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
        Task<IEnumerable<AttendanceLog>> GetAttendanceReport(DateTime date);

    }
}
