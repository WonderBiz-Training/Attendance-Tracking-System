using ATS.Data;
using ATS.IRepository;
using ATS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository
{
    public class AttendanceLogRepository : Repository<AttendanceLog>, IAttendanceLogRepository
    {
        private readonly ATSDbContext _dbContext;

        public AttendanceLogRepository(ATSDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
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

        public async Task<IEnumerable<AttendanceLog>> GetSummary(DateTime currentDate, string check)
        {
            try
            {
                var res = await _dbContext.attendanceLogs
                    .Where(log => log.AttendanceLogTime.Date == currentDate && log.CheckType == check)
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
    }
}
