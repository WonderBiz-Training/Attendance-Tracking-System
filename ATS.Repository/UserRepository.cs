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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ATSDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
