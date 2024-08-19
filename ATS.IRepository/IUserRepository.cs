using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUserByEmployeeIdAsync(long employeeDetailId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
