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
    public class GenderServices : IGenderServices
    {
        private readonly IGenderRepository _genderRepository;

        public GenderServices(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<GetGenderDto> CreateGenderAsync(CreateGenderDto genderDto)
        {
            try
            {
                var gender = await _genderRepository.CreateAsync(new Gender()
                {
                    GenderName = genderDto.GenderName,
                    GenderCode = genderDto.GenderName.ToUpper().Substring(0, 1),
                    CreatedBy = genderDto.CreatedBy,
                    UpdatedBy = genderDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var res = new GetGenderDto(
                    gender.Id,
                    gender.GenderName,
                    gender.GenderCode,
                    gender.IsActive
                );

                return res;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Gender already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteGenderAsync(long id)
        {
            try
            {
                var gender = await _genderRepository.GetAsync(id);

                if (gender == null)
                {
                    throw new Exception($"No Gender Found for id: {id}");
                }

                bool row = await _genderRepository.DeleteAsync(gender);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetGenderDto>> GetAllGendersAsync()
        {
            try
            {
                var genders = await _genderRepository.GetAllAsync();

                var gendersDto = genders.Select(gender => new GetGenderDto(
                    gender.Id,
                    gender.GenderName,
                    gender.GenderCode,
                    gender.IsActive
                ));

                return gendersDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetGenderDto> GetGenderAsync(long id)
        {
            try
            {
                var gender = await _genderRepository.GetAsync(id);

                if (gender == null)
                {
                    throw new Exception($"No Gender Found with id : {id}");
                }

                var genderDto = new GetGenderDto(
                    gender.Id,
                    gender.GenderName,
                    gender.GenderCode,
                    gender.IsActive
                );

                return genderDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetGenderDto> UpdateGenderAsync(long id, UpdateGenderDto genderDto)
        {
            try
            {
                var oldGender = await _genderRepository.GetAsync(id);

                if (oldGender == null)
                {
                    throw new Exception($"No Gender Found for id : {id}");
                }

                oldGender.GenderName = genderDto.GenderName;
                oldGender.GenderCode = genderDto.GenderName.ToUpper().Substring(0, 1);
                oldGender.IsActive = genderDto.IsActive ? genderDto.IsActive : true;
                oldGender.UpdatedBy = genderDto.UpdatedBy;
                oldGender.UpdatedAt = DateTime.Now;

                var gender = await _genderRepository.UpdateAsync(oldGender);

                var newGenderDto = new GetGenderDto(
                    gender.Id,
                    gender.GenderName,
                    gender.GenderCode,
                    gender.IsActive
                );

                return newGenderDto;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Gender already exists.");
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
