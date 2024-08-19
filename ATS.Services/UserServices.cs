using ATS.DTO;
using ATS.Hubs;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeDetailServices _employeeDetailServices;
        private readonly IHubContext<AtsHubs> _hubContext;

        public UserServices(IUserRepository userRepository, IEmployeeDetailServices employeeDetailServices, IHubContext<AtsHubs> hubContext)
        {
            _userRepository = userRepository;
            _employeeDetailServices = employeeDetailServices;
            _hubContext = hubContext;
        }

        public async Task<GetUserDto> CreateUserAsync(CreateUserDto UserDto)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(UserDto.Password);
                var users = await _userRepository.CreateAsync(new User
                {
                    Email = UserDto.Email,
                    Password = hashedPassword,
                    ContactNo = UserDto.ContactNo,
                    CreatedBy = UserDto.CreatedBy,
                    UpdatedBy = UserDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var createdUser = new GetUserDto(
                    users.Id,
                    users.Email,
                    users.Password,
                    users.ContactNo,
                    users.IsActive
                );

                await _hubContext.Clients.All.SendAsync("ReceiveUserUpdate", users.Email, users.Password, users.ContactNo);

                return createdUser;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This User already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            // Implement password verification during login or authentication
            //public bool VerifyPassword(string plainTextPassword, string hashedPassword)
            //{
            //    return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
            //}
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            try
            {
                var gender = await _userRepository.GetAsync(id);

                if (gender == null)
                {
                    throw new Exception($"No User Found for id: {id}");
                }

                bool row = await _userRepository.DeleteAsync(gender);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                var usersDto = users.Select(users => new GetUserDto(
                    users.Id,
                    users.Email,
                    users.Password,
                    users.ContactNo,
                    users.IsActive
                ));

                return usersDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetUserDto> GetUserAsync(long id)
        {
            try
            {
                var user = await _userRepository.GetAsync(id);

                if (user == null)
                {
                    throw new Exception($"No User Found with id : {id}");
                }

                var userDto = new GetUserDto(
                    user.Id,
                    user.Email,
                    user.Password,
                    user.ContactNo,
                    user.IsActive
                );

                return userDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetSignUpDto> SignUpUserAsync(SignUpDto signUpDto)
        {

            try
            {
                var users = await _userRepository.CreateAsync(new User
                {
                    Email = signUpDto.Email,
                    Password = signUpDto.Password,
                    ContactNo = signUpDto.ContactNo, 
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var createdUser = new GetUserDto(
                    users.Id,
                    users.Email,
                    users.Password,
                    users.ContactNo,
                    users.IsActive
                );


                var employeeInfo = await _employeeDetailServices.CreateEmployeeDetailAsync(
                    new CreateEmployeeDetailDto(
                        UserId: createdUser.Id,
                        EmployeeCode: signUpDto?.EmployeeCode,
                        FirstName: signUpDto.FirstName,
                        LastName: signUpDto.LastName,
                        ProfilePic: signUpDto.ProfilePic,
                        CreatedBy: createdUser.Id
                    )
                );


                var createEmployee = new GetSignUpDto(
                        createdUser.Id,
                        employeeInfo.FirstName,
                        employeeInfo.LastName,
                        createdUser.Email,
                        createdUser.ContactNo,
                        createdUser.Password,
                        employeeInfo.ProfilePic
                    );

                return createEmployee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GetUserDto> UpdateUserAsync(long id, UpdateUserDto UserDto)
        {
            try
            {
                var oldUser = await _userRepository.GetAsync(id);

                if (oldUser == null)
                {
                    throw new Exception($"No User Found for id : {id}");
                }

                if (!string.IsNullOrEmpty(UserDto.Password))
                {
                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(UserDto.Password);
                }

                oldUser.Email = UserDto.Email;
                //oldUser.Password = UserDto.Password;
                oldUser.ContactNo = UserDto.ContactNo;
                oldUser.IsActive = UserDto.IsActive;
                oldUser.UpdatedBy = UserDto.UpdatedBy;
                oldUser.UpdatedAt = DateTime.Now;

                var user = await _userRepository.UpdateAsync(oldUser);

                var newUserDto = new GetUserDto(
                    user.Id,
                    user.Email,
                    user.Password,
                    user.ContactNo,
                    user.IsActive
                );

                return newUserDto;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This User already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
