using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IPageServices
    {
        Task<IEnumerable<GetPageDto>> GetAllPagesAsync();

        Task<GetPageDto> GetPageAsync(long id);

        Task<GetPageDto> CreatePageAsync(CreatePageDto pageDto);

        Task<GetPageDto> UpdatePageAsync(long id, UpdatePageDto pageDto);

        Task<bool> DeletePageAsync(long id);
    }
}
