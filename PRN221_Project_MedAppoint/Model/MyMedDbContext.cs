using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PRN221_Project_MedAppoint.Model
{
    public class MyMedDbContext : DbContext
    {
        public MyMedDbContext(DbContextOptions<MyMedDbContext> options) : base(options)
        {
            //..
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersToSpecialist> UsersToSpecialists { get; set; }
        public DbSet<HealthInformation> HealthInformations { get; set; }
        public DbSet<DoctorDayOff> DoctorDayOffs { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<ElectronicMedicalRecords> ElectronicMedicalRecords { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<DoctorReviews> DoctorReviews { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<SpecialistToService> SpecialistToServices { get; set; }
    }
}
