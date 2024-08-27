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
    public class PageServices : IPageServices
    {
        private readonly IPageRepository _pageRepository;

        public PageServices(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<GetPageDto> CreatePageAsync(CreatePageDto pageDto)
        {
            try
            {
                var page = await _pageRepository.CreateAsync(new Page()
                {
                
                    PageCode = pageDto.PageCode,
                    PageTitle = pageDto.PageTitle,
                    CreatedBy = pageDto.CreatedBy,
                    UpdatedBy = pageDto.CreatedBy,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                var res = new GetPageDto(
                    page.Id,
                    page.PageCode,
                    page.PageTitle
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

        public async Task<bool> DeletePageAsync(long id)
        {
            try
            {
                var page = await _pageRepository.GetAsync(id);

                if (page == null)
                {
                    throw new Exception($"No Page Found for id: {id}");
                }

                bool row = await _pageRepository.DeleteAsync(page);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetPageDto>> GetAllPagesAsync()
        {
            try
            {
                var pages = await _pageRepository.GetAllAsync();

                var pagesDto = pages.Select(page => new GetPageDto(
                    page.Id,
                    page.PageCode,
                    page.PageTitle
                ));

                return pagesDto.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPageDto> GetPageAsync(long id)
        {
            try
            {
                var page = await _pageRepository.GetAsync(id);

                if (page == null)
                {
                    throw new Exception($"No Page Found with id : {id}");
                }

                var pageDto = new GetPageDto(
                    page.Id,
                    page.PageCode,
                    page.PageTitle
                   
                );

                return pageDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPageDto> UpdatePageAsync(long id, UpdatePageDto pageDto)
        {
            try
            {
                var oldPage = await _pageRepository.GetAsync(id);

                if (oldPage == null)
                {
                    throw new Exception($"No Page Found for id : {id}");
                }

                oldPage.PageCode = pageDto.PageCode;
                oldPage.PageTitle = pageDto.PageTitle;
                oldPage.UpdatedBy = pageDto.UpdatedBy;
                oldPage.UpdatedAt = DateTime.Now;

                var page = await _pageRepository.UpdateAsync(oldPage);

                var newPageDto = new GetPageDto(
                    page.Id,
                    page.PageCode,
                    page.PageTitle  
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
    }
}
