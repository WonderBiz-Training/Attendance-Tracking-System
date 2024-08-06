using ATS.DTO;
using ATS.IRepository;
using ATS.IServices;
using ATS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> CreateUserAsync(CreateUserDto UserDto)
        {
            try
            {
                var users = await _userRepository.CreateAsync(new User
                {
                    Email = UserDto.Email,
                    Password = UserDto.Password,
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

        public async Task<GetUserDto> UpdateUserAsync(long id, UpdateUserDto UserDto)
        {
            try
            {
                var oldUser = await _userRepository.GetAsync(id);

                if (oldUser == null)
                {
                    throw new Exception($"No User Found for id : {id}");
                }

                oldUser.Email = UserDto.Email;
                oldUser.Password = UserDto.Password;
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
