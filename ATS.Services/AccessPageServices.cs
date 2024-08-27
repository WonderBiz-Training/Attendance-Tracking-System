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
    public class AccessPageServices : IAccessPageServices
    {
        private readonly IAccessPageRepository _accessPageRepository;
        public AccessPageServices(IAccessPageRepository accessPageRepository)
        {
            _accessPageRepository = accessPageRepository;
        }

        public async Task<GetAccessPageDto> CreateAccessPageAsync(CreateAccessPageDto accessPageDto)
        {
            try
            {
                var page = await _accessPageRepository.CreateAsync(new AccessPage()
                {

                    RoleId = accessPageDto.RoleId,
                    PageId = accessPageDto.PageId,
                    CreatedBy = accessPageDto.CreatedBy,
                    UpdatedBy = accessPageDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var res = new GetAccessPageDto(
                    page.Id,
                    page.RoleId,
                    page.Role?.RoleName,
                    page.PageId,
                    page.Page?.PageTitle,
                    page.IsActive
                );

                return res;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Page already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<bool> DeleteAccessPageAsync(long id)
        {
            try
            {
                var page = await _accessPageRepository.GetAsync(id);

                if (page == null)
                {
                    throw new Exception($"No Page Found for id: {id}");
                }

                bool row = await _accessPageRepository.DeleteAsync(page);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetAccessPageDto> GetAccessPageAsync(long id)
        {
            try
            {
                var page = await _accessPageRepository.GetAsync(id);

                if (page == null)
                {
                    throw new Exception($"No Page Found with id : {id}");
                }

                var pageDto = new GetAccessPageDto(
                    page.Id,
                    page.RoleId,
                    page.Role.RoleName,
                    page.PageId,
                    page.Page.PageTitle,
                    page.IsActive
                );

                return pageDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetAccessPageDto>> GetAllAccessPagesAsync()
        {
            try
            {
                var pages = await _accessPageRepository.GetAllAsync();

                var pagesDto = pages.Select(page => new GetAccessPageDto(
                    page.Id,
                    page.RoleId,
                    page.Role.RoleName,
                    page.PageId,
                    page.Page.PageTitle,
                    page.IsActive
                ));

                return pagesDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetAccessPageDto> UpdateAccessPageAsync(long id, UpdateAccessPageDto accessPageDto)
        {
            try
            {
                var oldPage = await _accessPageRepository.GetAsync(id);

                if (oldPage == null)
                {
                    throw new Exception($"No Page Found for id : {id}");
                }

                oldPage.RoleId = (long)accessPageDto.RoleId;
                oldPage.PageId = (long)accessPageDto.PageId;
                oldPage.IsActive = accessPageDto.IsActive == 1 ? true : accessPageDto.IsActive == 0 ? false : oldPage.IsActive;
                oldPage.UpdatedBy = accessPageDto.UpdatedBy ?? 0;
                oldPage.UpdatedAt = DateTime.Now;

                var page = await _accessPageRepository.UpdateAsync(oldPage);

                var newPageDto = new GetAccessPageDto(
                    page.Id,
                    page.RoleId,
                    page.Role.RoleName,
                    page.PageId,
                    page.Page.PageTitle,
                    page.IsActive
                );

                return newPageDto;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("This Page already exists.");
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetAccessPageDto>> UpdateMultipleAccessPageAsync(List<UpdateMulitpleAccessPageDto> accessPageDto)
        {
            try
            {
                var updatedPages = new List<GetAccessPageDto>();

                foreach (var accessPage in accessPageDto)
                {
                    var oldPage = await _accessPageRepository.GetAsync(accessPage.Id);

                    if (oldPage == null)
                    {
                        throw new Exception($"No Page Found for id : {accessPage.Id}");
                    }

                    oldPage.RoleId = (long)accessPage.RoleId;
                    oldPage.PageId = (long)accessPage.PageId;
                    oldPage.IsActive = accessPage.IsActive == 1 ? true : accessPage.IsActive == 0 ? false : oldPage.IsActive;
                    oldPage.UpdatedBy = accessPage.UpdatedBy ?? 0;
                    oldPage.UpdatedAt = DateTime.Now;

                    var page = await _accessPageRepository.UpdateAsync(oldPage);

                    updatedPages.Add(new GetAccessPageDto(
                        page.Id,
                        page.RoleId,
                        page.Role?.RoleName,
                        page.PageId,
                        page.Page?.PageTitle,
                        page.IsActive
                    ));
                }

                Console.WriteLine("Updation");

                return updatedPages;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException?.Message.Contains("Cannot insert duplicate key row") == true ||
                    ex.InnerException?.Message.Contains("UNIQUE constraint failed") == true)
                {
                    throw new Exception("One or more pages already exist.");
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
