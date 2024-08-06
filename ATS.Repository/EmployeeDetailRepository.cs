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
    public class EmployeeDetailRepository : Repository<EmployeeDetail>, IEmployeeDetailRepository
    {
        private readonly ATSDbContext _dbContext;
        public EmployeeDetailRepository(ATSDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<IEnumerable<EmployeeDetail>> GetEmployeeDetailByUserId(long userId)
        {
            try
            {
                var employeeInfo = await _dbContext.employeeDetails
                    .Include(li => li.User)
                    .Include(li => li.Gender)
                    .Include(li => li.Designation)
                    .Where(li => li.UserId == userId)
                    .ToListAsync();

                return employeeInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDetail>> GetAllAsync()
        {
            try
            {
                var employeeInfo = await _dbContext.employeeDetails
                    .Include(li => li.User)
                    .Include(li => li.Gender)
                    .Include(li => li.Designation)
                    .ToListAsync();

                return employeeInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeDetail> GetAsync(long id)
        {
            try
            {
                var employeeInfo = await _dbContext.employeeDetails
                    .Include(li => li.User)
                    .Include(li => li.Gender)
                    .Include(li => li.Designation)
                    .FirstOrDefaultAsync(li => li.Id == id); ;

                return employeeInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
