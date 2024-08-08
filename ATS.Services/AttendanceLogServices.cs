using ATS.DTO;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using Microsoft.EntityFrameworkCore;
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

        public AttendanceLogServices(IAttendanceLogRepository attendanceLogRepository, IUserRepository userRepository, IEmployeeDetailRepository employeeDetailRepository)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _userRepository = userRepository;
            _employeeDetailRepository = employeeDetailRepository;
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

        //public async Task<GetActivityRecordDto> GetActivityRecord(long userId, DateTime? startDate, DateTime? endDate)
        //{
        //    try
        //    {
        //        var start = startDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)startDate;
        //        var end = endDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)endDate;

        //        IEnumerable<AttendanceLog> logs = await _attendanceLogRepository.GetActivityReport(userId, start, end);

        //        var periods = new List<List<AttendanceLog>>();
        //        List<AttendanceLog> currentPeriod = null;

        //        foreach (var log in logs)
        //        {
        //            if (currentPeriod == null || log.CheckType != currentPeriod.Last().CheckType)
        //            {
        //                if (currentPeriod != null)
        //                {
        //                    periods.Add(currentPeriod);
        //                }
        //                currentPeriod = new List<AttendanceLog> { log };
        //            }
        //            else
        //            {
        //                currentPeriod.Add(log);
        //            }
        //        }

        //        if (currentPeriod != null)
        //        {
        //            periods.Add(currentPeriod);
        //        }

        //        var totalInSeconds = periods
        //            .Select((period, index) => new { period, index })
        //            .Where(p => p.index < periods.Count - 1)
        //            .Select(p => new
        //            {
        //                CurrentPeriod = p.period,
        //                NextPeriod = periods[p.index + 1]
        //            })
        //            .Where(p => p.CurrentPeriod.Last().CheckType == "IN" && p.NextPeriod.Last().CheckType == "OUT")
        //            .Sum(p => (p.NextPeriod.Last().AttendanceLogTime - p.CurrentPeriod.Last().AttendanceLogTime).TotalSeconds);

        //        var totalInHours = TimeSpan.FromSeconds(totalInSeconds).ToString(@"hh\:mm\:ss");

        //        var totalOutSeconds = periods
        //            .Select((period, index) => new { period, index })
        //            .Where(p => p.index < periods.Count - 1)
        //            .Select(p => new
        //            {
        //                CurrentPeriod = p.period,
        //                NextPeriod = periods[p.index + 1]
        //            })
        //            .Where(p => p.CurrentPeriod.Last().CheckType == "IN" && p.NextPeriod.Last().CheckType == "OUT")
        //            .Sum(p => (p.NextPeriod.Last().AttendanceLogTime - p.CurrentPeriod.Last().AttendanceLogTime).TotalSeconds);

        //        var totalOutHours = TimeSpan.FromSeconds(totalOutSeconds).ToString(@"hh\:mm\:ss");

        //        var totalSeconds = periods
        //            .Select((period, index) => new { period, index })
        //            .Where(p => p.index < periods.Count - 1)
        //            .Select(p => new
        //            {
        //                CurrentPeriod = p.period,
        //                NextPeriod = periods[p.index + 1]
        //            })
        //            .Where(p => p.CurrentPeriod.Last().CheckType == "IN" && p.NextPeriod.Last().CheckType == "OUT")
        //            .Sum(p => (p.NextPeriod.Last().AttendanceLogTime - p.CurrentPeriod.Last().AttendanceLogTime).TotalSeconds);

        //        var totalHours = TimeSpan.FromSeconds(totalSeconds).ToString(@"hh\:mm\:ss");

        //        GetActivityRecordDto reportDto = new(total, totalHours, totalInHours, totalOutHours);

        //        return reportDto;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
                var start = startDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime) startDate;
                var end = endDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime) endDate;

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

        public async Task<GetTotalHours> GetTotalHoursOfEmployee(long userId, DateTime? startDate, DateTime? endDate)
        {
            var currentDate = startDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)startDate;
            var lastDate = endDate == DateTime.MinValue ? DateTime.Now.Date : (DateTime)endDate;

            // Fetch logs and specific employee details
            var logs = await _attendanceLogRepository.GetActivityReport(userId, currentDate, lastDate);
            var employee = await _employeeDetailRepository.GetEmployeeDetailByUserId(userId);
            var employeeDetail = employee.First();

            if (employeeDetail == null)
            {
                throw new Exception("Employee not found");
            }

            var firstCheckoutTime = logs
                .Where(log => log.CheckType == "OUT")
                .Select(log => log.AttendanceLogTime)
                .DefaultIfEmpty()
                .Min();

            var lastCheckInTime = logs
                .Where(log => log.CheckType == "IN" && log.AttendanceLogTime < firstCheckoutTime)
                .Select(log => log.AttendanceLogTime)
                .DefaultIfEmpty()
                .Max();

            var lastCheckoutTime = logs
                .Where(log => log.CheckType == "OUT")
                .Select(log => log.AttendanceLogTime)
                .DefaultIfEmpty()
                .Max();

            //var diff = (lastCheckoutTime - lastCheckInTime).TotalSeconds;

            //var totalHours = DateTime.Parse(DateTime.MinValue.Add(TimeSpan.FromSeconds(diff)).ToString("HH:mm:ss"));
            TimeSpan totalTimeSpan = lastCheckoutTime - lastCheckInTime;

            //string totalHours = totalTimeSpan.ToString("HH:mm:ss");

            var result = new GetTotalHours(
               employeeDetail.ProfilePic,
               employeeDetail.FirstName,
               employeeDetail.LastName,
               totalTimeSpan
            );

            return result;
        }
    }
}
