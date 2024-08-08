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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ATSDbContext _dbContext;
        public UserRepository(ATSDbContext dbcontext) : base(dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<IEnumerable<User>> GetUserByEmployeeIdAsync(long employeeDetailId)
        {
            try
            {
                var employeeInfo = await _dbContext.users
                    .Include(li => li.EmployeeDetail)
                    .Where(li => li.EmployeeDetailsId == employeeDetailId)
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
