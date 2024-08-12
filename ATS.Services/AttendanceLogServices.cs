using ATS.Data;
using ATS.DTO;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Services
{
    public class AttendanceLogServices : IAttendanceLogServices
    {
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeDetailRepository _employeeDetailRepository;
        private readonly ATSDbContext _dbContext;

        public AttendanceLogServices(IAttendanceLogRepository attendanceLogRepository, IUserRepository userRepository, IEmployeeDetailRepository employeeDetailRepository, ATSDbContext dbcontext)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _userRepository = userRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _dbContext = dbcontext;
        }
        public class TimePeriod
        {
            public DateTime InTime { get; set; }
            public DateTime OutTime { get; set; }
            public TimeSpan InHours { get; set; }
        }
        public async Task<GetAttendanceLogDto> CreateAttendanceLogAsync(CreateAttendanceLogDto attedanceLogDto)
        {
            try
            {
                var attendanceLog = await _attendanceLogRepository.CreateAsync(new AttendanceLog()
                {
                    UserId = attedanceLogDto.UserId,
                    AttendanceLogTime = attedanceLogDto.AttendanceLogTime,
                    CheckType = attedanceLogDto.CheckType,
                });

                var res = new GetAttendanceLogDto(
                    attendanceLog.Id,
                    attendanceLog.UserId,
                    attendanceLog.AttendanceLogTime,
                    attendanceLog.CheckType
                );

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAttendanceLogAsync(long id)
        {
            try
            {
                var attendanceLog = await _attendanceLogRepository.GetAsync(id);

                if (attendanceLog == null)
                {
                    throw new Exception($"No Log Found for id: {id}");
                }

                bool row = await _attendanceLogRepository.DeleteAsync(attendanceLog);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GetActivityRecordDto>> GetActivityRecord(long userId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var startDateParameter = new SqlParameter("@StartDate", SqlDbType.Date)
                {
                    Value = startDate.HasValue ? (object)startDate.Value : (object)DateTime.Now.Date
                };
                var endDateParameter = new SqlParameter("@EndDate", SqlDbType.Date)
                {
                    Value = endDate.HasValue ? (object)endDate.Value : (object)DateTime.Now.Date
                };

                var userIdParameter = new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = userId
                };

                var results = await _dbContext.Set<GetTotalInHours>()
                    .FromSqlRaw("EXECUTE dbo.GetAttendanceTimeDifferences @UserId, @StartDate, @EndDate", userIdParameter, startDateParameter, endDateParameter)
                    .ToListAsync();

                var dtoList = results.Select(model => new GetActivityRecordDto(
                    model.InTime,
                    model.OutTime,
                    model.TotalInHours
                ));

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetAttendanceLogDto>> GetAllAttendanceLogsAsync()
        {
            try
            {
                var attendanceLogs = await _attendanceLogRepository.GetAllAsync();

                var attendanceLogsDto = attendanceLogs.Select(attendanceLog => new GetAttendanceLogDto(
                    attendanceLog.Id,
                    attendanceLog.UserId,
                    attendanceLog.AttendanceLogTime,
                    attendanceLog.CheckType
                ));

                return attendanceLogsDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GetAttendanceLogDto> GetAttendanceLogAsync(long id)
        {
            try
            {
                var attendanceLog = await _attendanceLogRepository.GetAsync(id);

                if (attendanceLog == null)
                {
                    throw new Exception($"No Attendance Log Found with id : {id}");
                }

                var attendanceLogDto = new GetAttendanceLogDto(
                    attendanceLog.Id,
                    attendanceLog.UserId,
                    attendanceLog.AttendanceLogTime,
                    attendanceLog.CheckType
                );

                return attendanceLogDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GetAttendanceLogDto>> GetAttendanceLogByUserId(long userId)
        {
            try
            {
                var attendanceLog = await _attendanceLogRepository.GetAttendanceLogByUserId(userId);

                var attendanceLogDtos = attendanceLog.Select(attendanceLog => new GetAttendanceLogDto(
                    attendanceLog.Id,
                    attendanceLog.UserId,
                    attendanceLog.AttendanceLogTime,
                    attendanceLog.CheckType
                ));

                return attendanceLogDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GetAttendanceLogSummaryDto> GetAttendanceLogSummary(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var start = startDate == null ? DateTime.Now.Date : (DateTime)startDate;
                var end = endDate == null ? DateTime.Now.Date : (DateTime)endDate;

                IEnumerable<User> totalData = await _userRepository.GetAllAsync();

                var total = totalData.Count();

                IEnumerable<AttendanceLog> presentData = await _attendanceLogRepository.GetSummaryReport(start, end, "IN");

                var present = presentData.Count();

                IEnumerable<AttendanceLog> wfhData = await _attendanceLogRepository.GetSummaryReport(start, end, "WFH");

                var wfh = wfhData.Count();

                var absent = total - present;

                GetAttendanceLogSummaryDto summaryDto = new(total, present, wfh, absent);

                return summaryDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GetAttendanceLogDto> UpdateAttendanceLogAsync(long id, UpdateAttendanceLogDto attendanceLogDto)
        {
            try
            {
                var oldAttendanceLog = await _attendanceLogRepository.GetAsync(id);

                if (oldAttendanceLog == null)
                {
                    throw new Exception($"No AttendanceLog Found for id : {id}");
                }

                oldAttendanceLog.UserId = attendanceLogDto.UserId;
                oldAttendanceLog.AttendanceLogTime = attendanceLogDto.AttendanceLogTime;
                oldAttendanceLog.CheckType = attendanceLogDto.CheckType;

                var attendanceLog = await _attendanceLogRepository.UpdateAsync(oldAttendanceLog);

                var newAttendanceLogDto = new GetAttendanceLogDto(
                    attendanceLog.Id,
                    attendanceLog.UserId,
                    attendanceLog.AttendanceLogTime,
                    attendanceLog.CheckType
                );

                return newAttendanceLogDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<ATS.DTO.GetTotalHours> GetTotalHoursOfEmployee(DateTime? startDate, DateTime? endDate)
        {
            // Define parameters
            var startDateParameter = new SqlParameter("@StartDate", SqlDbType.Date)
            {
                Value = startDate.HasValue ? (object)startDate.Value : DateTime.Now.Date
            };
            var endDateParameter = new SqlParameter("@EndDate", SqlDbType.Date)
            {
                Value = endDate.HasValue ? (object)endDate.Value : DateTime.Now.Date
            };

            // Execute stored procedure and map results to the DTO
            var results = _dbContext.Set<ATS.Model.GetTotalHours>()
                .FromSqlRaw("EXECUTE dbo.GetTotalHour_Employee @StartDate, @EndDate", startDateParameter, endDateParameter)

                .ToList();

            // Map the results to the DTO
            var dtoList = results.Select(model => new ATS.DTO.GetTotalHours(
                model.LogDate,
                model.ProfilePic,
                model.FirstName,
                model.LastName,
                (model.LastCheckoutTime - model.LastCheckInTime).ToString(@"hh\:mm\:ss")
            ));

            return dtoList;
        }

        public async Task<IEnumerable<GetActivityRecordOutHoursDto>> GetActivityRecordOutHours(long userId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var startDateParameter = new SqlParameter("@StartDate", SqlDbType.Date)
                {
                    Value = startDate.HasValue ? (object)startDate.Value : (object)DateTime.Now.Date
                };
                var endDateParameter = new SqlParameter("@EndDate", SqlDbType.Date)
                {
                    Value = endDate.HasValue ? (object)endDate.Value : (object)DateTime.Now.Date
                };

                var userIdParameter = new SqlParameter("@UserId", SqlDbType.BigInt)
                {
                    Value = userId
                };

                var results = await _dbContext.Set<GetTotalOutHours>()
                    .FromSqlRaw("EXECUTE dbo.GetAttendanceOutTimeDifferences @UserId, @StartDate, @EndDate", userIdParameter, startDateParameter, endDateParameter)
                    .ToListAsync();

                var dtoList = results.Select(model => new GetActivityRecordOutHoursDto(
                    model.InTime,
                    model.OutTime,
                    model.TotalOutHours
                ));

                return dtoList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
