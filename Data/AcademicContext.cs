using Microsoft.EntityFrameworkCore;
using AcademicManagement.Models;

namespace AcademicManagement.Data
{
    public class AcademicContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TraineeCourse> CourseTrainees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Academic;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TraineeCourse>().HasKey(sc => new { sc.TraineeId, sc.CourseId });

            modelBuilder.Entity<TraineeCourse>()
                .HasOne<Trainee>(sc => sc.Trainee)
                .WithMany(s => s.TraineeCourses)
                .HasForeignKey(sc => sc.TraineeId);


            modelBuilder.Entity<TraineeCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.TraineeCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}