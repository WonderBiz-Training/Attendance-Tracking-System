using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<GetUserDto>> GetAllUsersAsync();

        Task<GetUserDto> GetUserAsync(long id);

        Task<GetUserDto> CreateUserAsync(CreateUserDto UserDto);

        Task<GetUserDto> UpdateUserAsync(long id, UpdateUserDto UserDto);

        Task<bool> DeleteUserAsync(long id);

        Task<GetSignUpDto> SignUpUserAsync(SignUpDto signUpDto);

        Task<GetUserDto> LogInUserAsync(LogInDto logInDto);

    }
}
