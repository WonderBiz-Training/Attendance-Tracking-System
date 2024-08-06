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

        public DbSet<EmployeeDetail> employeeDetails { get; set; }
        public DbSet<AttendanceLog> attendanceLogs { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<User> users { get; set; }
    }
}
