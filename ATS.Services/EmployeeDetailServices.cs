using ATS.DTO;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Services
{
    public class EmployeeDetailServices : IEmployeeDetailServices
    {
        private readonly IEmployeeDetailRepository _employeeDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IDesignationRepository _designationRepository;

        public EmployeeDetailServices(IEmployeeDetailRepository employeeDetailRepository, IUserRepository userRepository, IGenderRepository genderRepository, IDesignationRepository designationRepository)
        {
            _employeeDetailRepository = employeeDetailRepository;
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _designationRepository = designationRepository;
        }
        public async Task<GetEmployeeDetailDto> CreateEmployeeDetailAsync(CreateEmployeeDetailDto createEmployeeDetailDto)
        {
            try
            {
                var employeeInfo = await _employeeDetailRepository.CreateAsync(new EmployeeDetail()
                {
                    UserId = createEmployeeDetailDto.UserId,
                    GenderId = createEmployeeDetailDto.GenderId,
                    DesignationId = createEmployeeDetailDto.DesignationId,
                    EmployeeId = createEmployeeDetailDto.EmployeeId,
                    FirstName = createEmployeeDetailDto.FirstName,
                    LastName = createEmployeeDetailDto.LastName,
                    ProfilePic = createEmployeeDetailDto.ProfilePic,
                    CreatedBy = createEmployeeDetailDto.CreatedBy,
                    UpdatedBy = createEmployeeDetailDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var userDetail = await _userRepository.GetAsync(employeeInfo.UserId);
                userDetail.EmployeeDetailsId = employeeInfo.Id;

                await _userRepository.UpdateAsync(userDetail);

                var user = await _userRepository.GetAsync(employeeInfo.UserId);
                var gender = await _genderRepository.GetAsync(employeeInfo.GenderId);
                var designation = await _designationRepository.GetAsync(employeeInfo.DesignationId);

                if (gender != null && user != null && designation != null)
                {

                    var createdEmployeeDetail = new GetEmployeeDetailDto(
                        employeeInfo.Id,
                        user.Email,
                        gender.GenderName,
                        designation.DesignationName,
                        employeeInfo.EmployeeId,
                        employeeInfo.FirstName,
                        employeeInfo.LastName,
                        employeeInfo.ProfilePic
                    );

                    return createdEmployeeDetail;
                }
                else
                {
                    throw new Exception("Invalid Employee");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeDetailAsync(long id)
        {
            try
            {
                var employeeInfo = await _employeeDetailRepository.GetAsync(id);
                if (employeeInfo == null)
                {
                    throw new Exception($"Employee Details not found for id : {id}");
                }

                var deleted = await _employeeDetailRepository.DeleteAsync(employeeInfo);
                return deleted;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetEmployeeDetailDto> GetEmployeeDetailAsync(long id)
        {
            try
            {
                var employeeInfo = await _employeeDetailRepository.GetAsync(id);


                if (employeeInfo == null)
                {
                    throw new Exception($"Employee Details not found for id : {id}");
                }

                var employeeInfoDto = new GetEmployeeDetailDto(
                    employeeInfo.Id,
                    employeeInfo.User.Email,
                    employeeInfo.Gender.GenderName,
                    employeeInfo.Designation.DesignationName,
                    employeeInfo.EmployeeId,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic
                );

                return employeeInfoDto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetEmployeeDetailDto>> GetEmployeeDetailByUserId(long userId)
        {
            try
            {
                var employeeInfos = await _employeeDetailRepository.GetEmployeeDetailByUserId(userId);

                var employeeInfoDtos = employeeInfos.Select(employeeInfo => new GetEmployeeDetailDto(
                    employeeInfo.Id,
                    employeeInfo.User.Email,
                    employeeInfo.Gender.GenderName,
                    employeeInfo.Designation.DesignationName,
                    employeeInfo.EmployeeId,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic
                ));

                return employeeInfoDtos;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<IEnumerable<GetEmployeeDetailDto>> GetEmployeeDetailsAsync()
        {
            try
            {
                var employeeInfos = await _employeeDetailRepository.GetAllAsync();

                var employeeInfoDtos = employeeInfos.Select(employeeInfo => new GetEmployeeDetailDto(
                    employeeInfo.Id,
                    employeeInfo.User.Email,
                    employeeInfo.Gender.GenderName,
                    employeeInfo.Designation.DesignationName,
                    employeeInfo.EmployeeId,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic
                ));

                return employeeInfoDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetSearchDto> GetEmployeeDetailsWithFilter(string? firstName, string? lastName, string? employeeId, long? designationId, long? genderId, int? start, int? pageSize)
        {
            try
            {
                var fname = firstName ?? string.Empty;
                var lname = lastName ?? string.Empty;
                var empId = employeeId ?? string.Empty;
                var desgnId = designationId ?? 0;
                var genId = genderId ?? 0;
                var begin = start == 0 ? 1 : (int) start;
                var limit = pageSize == 0 ? 5 : (int) pageSize;


                var employeeInfos = await _employeeDetailRepository.GetEmployeeWithFilter(fname, lname, empId, desgnId, genId, begin, limit);

                var employeeInfoDtos = employeeInfos.Select(employeeInfo => new GetEmployeeDetailDto(
                    employeeInfo.Id,
                    employeeInfo.User.Email,
                    employeeInfo.Gender.GenderName,
                    employeeInfo.Designation.DesignationName,
                    employeeInfo.EmployeeId,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic
                   
                ));


                var res = new GetSearchDto(
                    employeeInfoDtos.Count(),
                    employeeInfoDtos
                );

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<GetEmployeeDetailDto> UpdateEmployeeDetailAsync(long id, UpdateEmployeeDetailDto updateEmployeeDetailDto)
        {
            try
            {
                var oldemployeeInfo = await _employeeDetailRepository.GetAsync(id);
                if (oldemployeeInfo == null)
                {
                    throw new Exception($"Employee Details found for id : {id}");
                }

                oldemployeeInfo.UserId = updateEmployeeDetailDto.UserId;
                oldemployeeInfo.GenderId = updateEmployeeDetailDto.GenderId;
                oldemployeeInfo.DesignationId = updateEmployeeDetailDto.DesignationId;
                oldemployeeInfo.EmployeeId = updateEmployeeDetailDto.EmployeeId;
                oldemployeeInfo.FirstName = updateEmployeeDetailDto.FirstName;
                oldemployeeInfo.LastName = updateEmployeeDetailDto.LastName;
                oldemployeeInfo.ProfilePic = updateEmployeeDetailDto.ProfilePic;
                oldemployeeInfo.UpdatedBy = updateEmployeeDetailDto.UpdatedBy;
                oldemployeeInfo.UpdatedAt = DateTime.Now;

                await _employeeDetailRepository.UpdateAsync(oldemployeeInfo);

                var user = await _userRepository.GetAsync(oldemployeeInfo.UserId);
                var gender = await _genderRepository.GetAsync(oldemployeeInfo.GenderId);
                var designation = await _designationRepository.GetAsync(oldemployeeInfo.DesignationId);

                if (gender != null && user != null && designation != null)
                {

                    var updatedemployeeInfo = new GetEmployeeDetailDto(
                        oldemployeeInfo.Id,
                        user.Email,
                        gender.GenderName,
                        designation.DesignationName,
                        oldemployeeInfo.EmployeeId,
                        oldemployeeInfo.FirstName,
                        oldemployeeInfo.LastName,
                        oldemployeeInfo.ProfilePic
                    );

                    return updatedemployeeInfo;
                }
                else
                {
                    throw new Exception("Invalid Employee");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
