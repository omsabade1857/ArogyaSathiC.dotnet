using AarogyaSaathi.Dto;
using AarogyaSaathi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AarogyaSaathi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> PatientData { get; set; }
        public DbSet<RoleStore> RoleStore { get; set; }
        public DbSet<AarogyaSaathi.Dto.BookingView>? BookingView { get; set; }
        public DbSet<Appointment> AppointmentData { get; set; }
        public DbSet<PatientHistory> PatientHistory { get; set; }
        public DbSet<Doctor> DoctorData { get; set; }
    }
}
