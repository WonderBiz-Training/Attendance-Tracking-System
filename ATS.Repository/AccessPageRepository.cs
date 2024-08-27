using ATS.Data;
using ATS.IRepository;
using ATS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository
{
    public class AccessPageRepository : Repository<AccessPage>, IAccessPageRepository
    {
        public AccessPageRepository(ATSDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
