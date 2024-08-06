using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IEmployeeDetailServices
    {
        Task<IEnumerable<GetEmployeeDetailDto>> GetEmployeeDetailsAsync();
        Task<GetEmployeeDetailDto> GetEmployeeDetailAsync(long id);
        Task<GetEmployeeDetailDto> CreateEmployeeDetailAsync(CreateEmployeeDetailDto createEmployeeDetailDto);
        Task<GetEmployeeDetailDto> UpdateEmployeeDetailAsync(long id, UpdateEmployeeDetailDto updateEmployeeDetailDto);
        Task<bool> DeleteEmployeeDetailAsync(long id);
        Task<IEnumerable<GetEmployeeDetailDto>> GetEmployeeDetailByUserId(long userId);
    }
}
