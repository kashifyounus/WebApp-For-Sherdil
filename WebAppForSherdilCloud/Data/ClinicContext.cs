using Microsoft.EntityFrameworkCore;
using WebAppForSherdilCloud.Models;

namespace WebAppForSherdilCloud.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Doctor>()
             .HasIndex(u => u.Email)
             .IsUnique();
            builder.Entity<Patient>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
