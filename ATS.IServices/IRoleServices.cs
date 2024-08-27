using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IRoleServices
    {
        Task<IEnumerable<GetRoleDto>> GetAllRolesAsync();

        Task<GetRoleDto> GetRoleAsync(long id);

        Task<GetRoleDto> CreateRoleAsync(CreateRoleDto roleDto);

        Task<GetRoleDto> UpdateRoleAsync(long id, UpdateRoleDto roleDto);

        Task<bool> DeleteRoleAsync(long id);
    }
}
