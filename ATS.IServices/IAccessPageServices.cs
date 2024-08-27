using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IAccessPageServices
    {
        Task<IEnumerable<GetAccessPageDto>> GetAllAccessPagesAsync();

        Task<GetAccessPageDto> GetAccessPageAsync(long id);

        Task<GetAccessPageDto> CreateAccessPageAsync(CreateAccessPageDto accessPageDto);

        Task<GetAccessPageDto> UpdateAccessPageAsync(long id, UpdateAccessPageDto accessPageDto);

        Task<bool> DeleteAccessPageAsync(long id);
    }
}
