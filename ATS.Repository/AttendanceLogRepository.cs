using ATS.Data;
using ATS.IRepository;
using ATS.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ATS.Repository
{
    public class AttendanceLogRepository : Repository<AttendanceLog>, IAttendanceLogRepository
    {
        private readonly ATSDbContext _dbContext;

        public AttendanceLogRepository(ATSDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<IEnumerable<GetStatusOfAttendanceLog>> GetAllStatusOfAttendanceLog()
        {
            try
            {
                var data = await _dbContext.Set<GetStatusOfAttendanceLog>()
                               .FromSqlRaw("EXECUTE dbo.GetEmployeeAttendanceSummary")
                               .ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<GetStatusOfAttendanceLog>> GetPacificStatusOfAttendanceLog(string firstName)
        {
            try
            {
                var firstNameParameter = new SqlParameter("@FirstName", firstName);
                var data = await _dbContext.Set<GetStatusOfAttendanceLog>()
                    .FromSqlRaw("EXECUTE dbo.GetPacificStatus @FirstName", firstNameParameter)
                    .ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<AttendanceLog>> GetAttendanceLogByUserId(long userId)
        {
            try
            {
                var attendanceLogs = await _dbContext.attendanceLogs
                    .Include(li => li.User)
                    .Where(li => li.UserId == userId)
                    .ToListAsync();

                return attendanceLogs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AttendanceLog>> GetAllAttendanceLogs()
        {
            try
            {
                var attendanceLogs = await _dbContext.attendanceLogs
                    .Include(log => log.User).ThenInclude(user => user.EmployeeDetail)
                    .ToListAsync();

                return attendanceLogs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AttendanceLog>> GetAttendanceReport(DateTime date)
        {
            try
            {
                var res = await _dbContext.attendanceLogs
                    .Where(log => log.AttendanceLogTime.Date >= date)
                    .GroupBy(log => log.UserId)
                    .Select(group => group.OrderBy(log => log.AttendanceLogTime).FirstOrDefault())
                    .ToListAsync();

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AttendanceLog>> GetSummaryReport(DateTime startDate, DateTime endDate, string check)
        {
            try
            {
                var res = await _dbContext.attendanceLogs
                    .Where(log => log.AttendanceLogTime.Date >= startDate && log.AttendanceLogTime.Date <= endDate && log.CheckType == check)
                    .GroupBy(log => log.UserId)
                    .Select(group => group.OrderBy(log => log.AttendanceLogTime).FirstOrDefault())
                    .ToListAsync();

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AttendanceLog>> GetActivityReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var res = await _dbContext.attendanceLogs
                    .Where(log => log.AttendanceLogTime.Date >= startDate && log.AttendanceLogTime.Date <= endDate)
                    .OrderBy(log => log.AttendanceLogTime)
                    .ToListAsync();

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GetTotalInHours>> GetTotalInHours(long? userId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var startDateParameter = new SqlParameter("@StartDate", startDate);
                var endDateParameter = new SqlParameter("@EndDate", endDate);

                var userIdParameter = new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = userId
                };

                var results = await _dbContext.Set<GetTotalInHours>()
                            .FromSqlRaw("EXECUTE dbo.GetAttendanceInTimeDifferences @UserId, @StartDate, @EndDate", userIdParameter, startDateParameter, endDateParameter)
                            .ToListAsync();
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<GetTotalOutHours>> GetTotalOutHours(long? userId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var startDateParameter = new SqlParameter("@StartDate", startDate);
                var endDateParameter = new SqlParameter("@EndDate", endDate);

                var userIdParameter = new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = userId
                };

                var results = await _dbContext.Set<GetTotalOutHours>()
                    .FromSqlRaw("EXECUTE dbo.GetAttendanceOutTimeDifferences @UserId, @StartDate, @EndDate", userIdParameter, startDateParameter, endDateParameter)
                    .ToListAsync();

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
