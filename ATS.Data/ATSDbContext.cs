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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetTotalHours>(entity =>
            {
                entity.HasNoKey(); // Indicate that this entity does not have a key
                entity.ToView(null); // Optional: specify that this entity does not map to a database view
            });

            modelBuilder.Entity<GetTotalOutHours>(entity =>
            {
                entity.HasNoKey(); // Indicate that this entity does not have a key
                entity.ToView(null); // Optional: specify that this entity does not map to a database view
            });

            modelBuilder.Entity<GetTotalInHours>(entity =>
            {
                entity.HasNoKey(); // Indicate that this entity does not have a key
                entity.ToView(null); // Optional: specify that this entity does not map to a database view
            });

            modelBuilder.Entity<GetStatusOfAttendanceLog>(entity =>
            {
                entity.HasNoKey(); 
                entity.ToView(null); 
            });
        }

        public DbSet<EmployeeDetail> employeeDetails { get; set; }
        public DbSet<AttendanceLog> attendanceLogs { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<User> users { get; set; }
    }
}
