using ATS.DTO;
using ATS.Hubs;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<AtsHubs> _hubContext;

        public EmployeeDetailServices(IEmployeeDetailRepository employeeDetailRepository, IUserRepository userRepository, IGenderRepository genderRepository, IDesignationRepository designationRepository,IHubContext<AtsHubs> hubContext)
        {
            _employeeDetailRepository = employeeDetailRepository;
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _designationRepository = designationRepository;
            _hubContext = hubContext;
        }
        public async Task<GetEmployeeDetailDto> CreateEmployeeDetailAsync(CreateEmployeeDetailDto createEmployeeDetailDto)
        {
            try
            {
                var employeeInfo = await _employeeDetailRepository.CreateAsync(new EmployeeDetail()
                {
                    UserId = createEmployeeDetailDto.UserId,
                    //GenderId = createEmployeeDetailDto.GenderId,
                    //DesignationId = createEmployeeDetailDto.DesignationId,
                    EmployeeCode = createEmployeeDetailDto?.EmployeeCode,
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
                        employeeInfo.UserId,
                        user.Email,
                        //gender.GenderName,
                        //designation.DesignationName,
                        //employeeInfo.EmployeeCode,
                        employeeInfo.FirstName,
                        employeeInfo.LastName,
                        employeeInfo.ProfilePic,
                        user.ContactNo
                    );

                    await _hubContext.Clients.All.SendAsync("ReceiveEmployeeUpdate", employeeInfo.UserId, employeeInfo.EmployeeCode, employeeInfo.FirstName, employeeInfo.LastName,employeeInfo.DesignationId, employeeInfo.GenderId, employeeInfo.ProfilePic);

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
                    employeeInfo.UserId,
                    employeeInfo.User.Email,
                    //employeeInfo.Gender.GenderName,
                    //employeeInfo.Designation.DesignationName,
                    //employeeInfo.EmployeeCode,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic,
                    employeeInfo.User.ContactNo
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
                    employeeInfo.UserId,
                    employeeInfo.User.Email,
                    //employeeInfo.Gender.GenderName,
                    //employeeInfo.Designation.DesignationName,
                    //employeeInfo.EmployeeCode,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic,
                    employeeInfo.User.ContactNo
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
                    employeeInfo.UserId,
                    employeeInfo.User.Email,
                    //employeeInfo.Gender.GenderName,
                    //employeeInfo.Designation.DesignationName,
                    //employeeInfo.EmployeeCode,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic,
                    employeeInfo.User.ContactNo
                ));

                return employeeInfoDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetSearchDto> GetEmployeeDetailsWithFilter(string? firstName, string? lastName, string? employeeCode, long? designationId, long? genderId, int? start, int? pageSize)
        {
            try
            {
                var fname = firstName ?? string.Empty;
                var lname = lastName ?? string.Empty;
                var empCode = employeeCode ?? string.Empty;
                var desgnId = designationId ?? 0;
                var genId = genderId ?? 0;
                var begin = start == 0 ? 1 : (int) start;
                var limit = pageSize == 0 ? 5 : (int) pageSize;


                var employeeInfos = await _employeeDetailRepository.GetEmployeeWithFilter(fname, lname, empCode, desgnId, genId, begin, limit);

                var employeeInfoDtos = employeeInfos.Select(employeeInfo => new GetEmployeeDetailDto(
                    employeeInfo.Id,
                    employeeInfo.UserId,
                    employeeInfo.User.Email,
                    //employeeInfo.Gender.GenderName,
                    //employeeInfo.Designation.DesignationName,
                    //employeeInfo.EmployeeCode,
                    employeeInfo.FirstName,
                    employeeInfo.LastName,
                    employeeInfo.ProfilePic,
                    employeeInfo.User.ContactNo
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

        public async Task<IEnumerable<GetFaceEncodingDto>> GetFaceEncodingsAsync()
        {
            try
            {
                var employeeDetails = await _employeeDetailRepository.GetAllAsync();

                var employeeInfoDtos = employeeDetails.Select(employeeInfo => new GetFaceEncodingDto(
                    employeeInfo.Id,
                    employeeInfo.UserId,
                    employeeInfo.FirstName,
                    employeeInfo.FaceEncoding

                ));

                return employeeInfoDtos;
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
                    throw new Exception($"Employee Details not found for id : {id}");
                }

                oldemployeeInfo.EmployeeCode = updateEmployeeDetailDto.EmployeeCode ?? oldemployeeInfo.EmployeeCode;
                oldemployeeInfo.FirstName = string.IsNullOrEmpty(updateEmployeeDetailDto.FirstName) ? oldemployeeInfo.FirstName : updateEmployeeDetailDto.FirstName;
                oldemployeeInfo.LastName = string.IsNullOrEmpty(updateEmployeeDetailDto.LastName) ? oldemployeeInfo.LastName : updateEmployeeDetailDto.LastName;
                oldemployeeInfo.ProfilePic = string.IsNullOrEmpty(updateEmployeeDetailDto.ProfilePic) ? oldemployeeInfo.ProfilePic : updateEmployeeDetailDto.ProfilePic;
                oldemployeeInfo.FaceEncoding = updateEmployeeDetailDto.FaceEncoding != null && updateEmployeeDetailDto.FaceEncoding.Any()
                        ? updateEmployeeDetailDto.FaceEncoding.Select(b => b ?? 0).ToArray()
                        : oldemployeeInfo.FaceEncoding;
                oldemployeeInfo.UpdatedBy = updateEmployeeDetailDto.UpdatedBy ?? 0;
                oldemployeeInfo.UpdatedAt = DateTime.Now;

                await _employeeDetailRepository.UpdateAsync(oldemployeeInfo);

                var user = await _userRepository.GetAsync(oldemployeeInfo.UserId);
                var gender = await _genderRepository.GetAsync(oldemployeeInfo.GenderId);
                var designation = await _designationRepository.GetAsync(oldemployeeInfo.DesignationId);

                if (gender != null && user != null && designation != null)
                {
                    var updatedemployeeInfo = new GetEmployeeDetailDto(
                        oldemployeeInfo.Id,
                        oldemployeeInfo.UserId,
                        user.Email,
                        //gender.GenderName,
                        //designation.DesignationName,
                        //oldemployeeInfo.EmployeeCode,
                        oldemployeeInfo.FirstName,
                        oldemployeeInfo.LastName,
                        oldemployeeInfo.ProfilePic,
                        user.ContactNo
                    );

                    await _hubContext.Clients.All.SendAsync("ReceiveUpdateEncoding", updatedemployeeInfo.Id, updatedemployeeInfo.UserId ,updateEmployeeDetailDto.FirstName, updatedemployeeInfo.LastName, updateEmployeeDetailDto.ProfilePic, updateEmployeeDetailDto.FaceEncoding);


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
