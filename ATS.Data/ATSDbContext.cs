using ATS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data
{
    public class ATSDbContext : DbContext
    {
        public ATSDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Designation> designations { get; set; }
    }
}
