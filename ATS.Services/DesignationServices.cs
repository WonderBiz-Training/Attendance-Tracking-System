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
    public class DesignationServices : IDesignationServices
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationServices(IDesignationRepository DesignationRepository)
        {
            _designationRepository = DesignationRepository;
        }

        public async Task<GetDesignationDto> CreateDesignationAsync(CreateDesignationDto designationDto)
        {
            try
            {
                var Designation = await _designationRepository.CreateAsync(new Designation
                {
                    DesignationName = designationDto.DesignationName,
                    DesignationCode = designationDto.DesignationCode,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var createdDesignation = new GetDesignationDto(Designation.Id, Designation.DesignationName, Designation.DesignationCode, Designation.IsActive);
                return createdDesignation;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Designation already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDesignationAsync(long id)
        {
            try
            {
                var Designation = await _designationRepository.GetAsync(id);
                if (Designation == null)
                {
                    throw new Exception($"Designation not found for id : {id}");
                }

                var deleted = await _designationRepository.DeleteAsync(Designation);
                return deleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetDesignationDto>> GetAllDesignationsAsync()
        {
            try
            {
                var designations = await _designationRepository.GetAllAsync();

                var designationDtos = designations.Select(Designation => new GetDesignationDto(
                    Designation.Id,
                    Designation.DesignationName,
                    Designation.DesignationCode,
                    Designation.IsActive
                ));

                return designationDtos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetDesignationDto> GetDesignationByIdAsync(long id)
        {
            try
            {
                var Designation = await _designationRepository.GetAsync(id);
                if (Designation == null)
                {
                    throw new Exception($"Designation not found for id : {id}");
                }

                var DesignationDto = new GetDesignationDto(Designation.Id, Designation.DesignationName, Designation.DesignationCode, Designation.IsActive);
                return DesignationDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetDesignationDto> UpdateDesignationAsync(long id, UpdateDesignationDto designationDto)
        {
            try
            {
                var oldDesignation = await _designationRepository.GetAsync(id);
                if (oldDesignation == null)
                {
                    throw new Exception($"Designation not found for id : {id}");
                }

                oldDesignation.DesignationName = designationDto.DesignationName;
                oldDesignation.DesignationCode = designationDto.DesignationCode;
                oldDesignation.IsActive = designationDto.IsActive;
                oldDesignation.UpdatedAt = DateTime.Now;

                await _designationRepository.UpdateAsync(oldDesignation);

                var updatedDesignation = new GetDesignationDto(oldDesignation.Id, oldDesignation.DesignationName, oldDesignation.DesignationCode, oldDesignation.IsActive);
                return updatedDesignation;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Designation already exists.");
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
