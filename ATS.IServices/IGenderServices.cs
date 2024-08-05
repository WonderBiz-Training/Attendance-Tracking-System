using ATS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IServices
{
    public interface IGenderServices
    {
        Task<IEnumerable<GetGenderDto>> GetAllGendersAsync();

        Task<GetGenderDto> GetGenderAsync(long id);

        Task<GetGenderDto> CreateGenderAsync(CreateGenderDto genderDto);

        Task<GetGenderDto> UpdateGenderAsync(long id, UpdateGenderDto genderDto);

        Task<bool> DeleteGenderAsync(long id);
    }
}
