using ATS.Data;
using ATS.IRepository;
using ATS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository
{
    public class AccessPageRepository : Repository<AccessPage>, IAccessPageRepository
    {
        private readonly ATSDbContext _dbContext;
        public AccessPageRepository(ATSDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<AccessPage> GetAsync(long id)
        {
            try
            {
                var employeeInfo = await _dbContext.accessPages
                    .Include(li => li.Role)
                    .Include(li => li.Page)
                    .FirstOrDefaultAsync(li => li.Id == id);

                return employeeInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AccessPage>> GetAllAsync()
        {
            try
            {
                var employeeInfo = await _dbContext.accessPages
                    .Include(li => li.Role)
                    .Include(li => li.Page)
                    .ToListAsync();

                return employeeInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
