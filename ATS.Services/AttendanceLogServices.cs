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
                var start = startDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)startDate;
                var end = endDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)endDate;

                IEnumerable<AttendanceLog> logs = await _attendanceLogRepository.GetActivityReport(start, end);

                var periods = new List<List<AttendanceLog>>();
                List<AttendanceLog> currentPeriod = null;

                foreach (var log in logs)
                {
                    if (currentPeriod == null || log.CheckType != currentPeriod.Last().CheckType)
                    {
                        if (currentPeriod != null)
                        {
                            periods.Add(currentPeriod);
                        }
                        currentPeriod = new List<AttendanceLog> { log };
                    }
                    else
                    {
                        currentPeriod.Add(log);
                    }
                }

                if (currentPeriod != null)
                {
                    periods.Add(currentPeriod);
                }

                var indRec = periods
                    .Select((period, index) => new { period, index })
                    .Where(p => p.index < periods.Count - 1)
                    .Select(p => new
                    {
                        CurrentPeriod = p.period,
                        NextPeriod = periods[p.index + 1]
                    })
                    .Where(p => p.CurrentPeriod.Last().CheckType == "IN" && p.NextPeriod.Last().CheckType == "OUT")
                    .Select(p => new TimePeriod
                    {
                        InTime = p.CurrentPeriod.Last().AttendanceLogTime,
                        OutTime = p.NextPeriod.Last().AttendanceLogTime,
                        InHours = p.NextPeriod.Last().AttendanceLogTime - p.CurrentPeriod.Last().AttendanceLogTime
                    });

                var reportDto = indRec.Select(rec => new GetActivityRecordDto(
                    rec.InTime,
                    rec.OutTime,
                    rec.InHours
                )).ToList();

                return reportDto;
            }
            catch (Exception)
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
                var start = startDate == null ? DateTime.Now.Date : (DateTime) startDate;
                var end = endDate == null ? DateTime.Now.Date : (DateTime) endDate;

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

        /*public async Task<GetAttendanceLogSummaryDto> GetAttendanceReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var start = startDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)startDate;
                var end = endDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)endDate;

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
        }*/

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
            var startDateParameter = new SqlParameter("@StartDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value);
            var endDateParameter = new SqlParameter("@EndDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value);

            // Execute stored procedure and map results to the DTO
            var results = _dbContext.Set<ATS.Model.GetTotalHours>()
                .FromSqlRaw("EXECUTE dbo.GetTotalHoursOfUsers @StartDate, @EndDate", startDateParameter, endDateParameter)
                .ToList();

            // Map the results to the DTO
            var dtoList = results.Select(model => new ATS.DTO.GetTotalHours(

                model.LogDate,  // Ensure this is correct; should probably be a URL or path if it's an image
                model.LastCheckInTime, // Ensure the correct format
                model.LastCheckoutTime,// Ensure the correct format
                (model.LastCheckoutTime - model.LastCheckInTime).ToString(@"hh\:mm\:ss") // Correct format for total hours
            ));

            return dtoList;
        }



        //public async Task<IEnumerable<GetTotalHours>> GetTotalHoursOfEmployee2(DateTime? startDate, DateTime? endDate)
        //{
        //    // Define the date range, defaulting to the current date if not provided
        //    var currentDate = startDate ?? DateTime.Now.Date;
        //    var lastDate = endDate ?? DateTime.Now.Date;

        //    var myList = new List<GetTotalHours>();
        //    // Fetch all users
        //    var users = await _userRepository.GetAllAsync();
        //    foreach(var user in users)
        //    {
        //        var parameters = new[]
        //            {
        //                new SqlParameter("@StartDate", currentDate),
        //                new SqlParameter("@EndDate", lastDate)
        //            };

        //        // Call the stored procedure and get the result
        //      //  var res = await _dbContext.Database.SqlQueryRaw($"EXECUTE GetTotalHoursOfUsers EXEC GetTotalHoursOfUsers @StartDate, @EndDate", parameters);
        //       // var result = await _dbContext.Set<AttendanceLog>().FromSqlRaw("EXEC GetTotalHoursOfUsers @StartDate, @EndDate", parameters)
        //            //.ToListAsync();

        //        // Assuming the result has a single entry and mapping it to GetTotalHours
        //        var totalHours = res.Select(row => new GetTotalHours(
        //            row.User.EmployeeDetail.ProfilePic,
        //            row.User.EmployeeDetail.FirstName,
        //            row.User.EmployeeDetail.LastName,
        //            row.User.EmployeeDetail.EmployeeId
        //        )).FirstOrDefault();
        //        myList.Add(totalHours);
        //    }
        // Process each user to get their total hours
        /*var totalHoursTasks = users.Select(async (user) =>
        {

            try
            {


                // Define parameters for the stored procedure
                var parameters = new[]
                {
                    new SqlParameter("@StartDate", currentDate),
                    new SqlParameter("@EndDate", lastDate)
                };

                // Call the stored procedure and get the result
                var result = await _dbContext.Set<AttendanceLog>().FromSqlRaw("EXEC GetTotalHoursOfUsers @StartDate, @EndDate", parameters)
                    .ToListAsync();

                // Assuming the result has a single entry and mapping it to GetTotalHours
                var totalHours = result.Select(row => new GetTotalHours(
                    row.User.EmployeeDetail.ProfilePic,
                    row.User.EmployeeDetail.FirstName,
                    row.User.EmployeeDetail.LastName,
                    row.User.EmployeeDetail.EmployeeId
                )).FirstOrDefault();
                myList.Add(totalHours);
                //return result;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error processing user {user.Id}: {ex.Message}");
                return null;
            }
        });
        */
        // Await all tasks and filter out null results
        // var results = await Task.WhenAll(totalHoursTasks);
        //return results.Where(result => result != null);
        // return myList;
        // }





    }
}
