using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.IRepository
{
    public interface IAccessPageRepository : IRepository<AccessPage>
    {
        Task<IEnumerable<AccessPage>> GetAccessByRoleId(long roleId);
    }
}
