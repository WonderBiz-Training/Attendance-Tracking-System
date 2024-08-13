using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IRepository
{
    public interface IEmployeeDetailRepository : IRepository<EmployeeDetail>
    {
        Task<IEnumerable<EmployeeDetail>> GetEmployeeDetailByUserId(long userId);

        Task<IEnumerable<EmployeeDetail>> GetEmployeeWithFilter(string firstName, string lastName, string employeeCode, long designationId, long genderId, int start, int pageSize);

    }
}
