using ATS.Data;
using ATS.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ATSDbContext _dbcontext;

        public Repository(ATSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _dbcontext.Set<T>().AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dbcontext.Remove(entity);
                var row = await _dbcontext.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var res = await _dbcontext.Set<T>().ToListAsync();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetAsync(long id)
        {
            try
            {
                var res = await _dbcontext.Set<T>().FindAsync(id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbcontext.Set<T>().Update(entity);
                await _dbcontext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
