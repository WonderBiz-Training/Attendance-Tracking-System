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
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<GetRoleDto> CreateRoleAsync(CreateRoleDto roleDto)
        {
            try
            {
                var role = await _roleRepository.CreateAsync(new Role()
                {
                    RoleName = roleDto.RoleName,
                    CreatedBy = roleDto.CreatedBy,
                    UpdatedBy = roleDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var res = new GetRoleDto(
                    role.Id,
                    role.RoleName,
                    role.IsActive
                );

                return res;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Role already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRoleAsync(long id)
        {
            try
            {
                var role = await _roleRepository.GetAsync(id);

                if (role == null)
                {
                    throw new Exception($"No Role Found for id: {id}");
                }

                bool row = await _roleRepository.DeleteAsync(role);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetRoleDto>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();

                var rolesDto = roles.Select(role => new GetRoleDto(
                    role.Id,
                    role.RoleName,
                    role.IsActive
                ));

                return rolesDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetRoleDto> GetRoleAsync(long id)
        {
            try
            {
                var role = await _roleRepository.GetAsync(id);

                if (role == null)
                {
                    throw new Exception($"No Role Found with id : {id}");
                }

                var roleDto = new GetRoleDto(
                    role.Id,
                    role.RoleName,
                    role.IsActive
                );

                return roleDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetRoleDto> UpdateRoleAsync(long id, UpdateRoleDto roleDto)
        {
            try
            {
                var oldRole = await _roleRepository.GetAsync(id);

                if (oldRole == null)
                {
                    throw new Exception($"No Role Found for id : {id}");
                }

                oldRole.RoleName = roleDto.RoleName;
                oldRole.IsActive = roleDto.IsActive ? roleDto.IsActive : true;
                oldRole.UpdatedBy = roleDto.UpdatedBy;
                oldRole.UpdatedAt = DateTime.Now;

                var role = await _roleRepository.UpdateAsync(oldRole);

                var newRoleDto = new GetRoleDto(
                    role.Id,
                    role.RoleName,
                    role.IsActive
                );

                return newRoleDto;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Role already exists.");
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
