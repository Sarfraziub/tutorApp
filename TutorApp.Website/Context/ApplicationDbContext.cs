using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TutorApp.Website.Models;

namespace TutorApp.Website.Context
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StudentFile> Files { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for classes
            modelBuilder.Entity<Class>().HasData(
                new Class { ClassId = 1, ClassName = "Class 1" },
                new Class { ClassId = 2, ClassName = "Class 2" },
                new Class { ClassId = 3, ClassName = "Class 3" },
                new Class { ClassId = 4, ClassName = "Class 4" },
                new Class { ClassId = 5, ClassName = "Class 5" },
                new Class { ClassId = 6, ClassName = "Class 6" },
                new Class { ClassId = 7, ClassName = "Class 7" },
                new Class { ClassId = 8, ClassName = "Class 8" },
                new Class { ClassId = 9, ClassName = "Class 9" },
                new Class { ClassId = 10, ClassName = "Class 10" }
            );

            // Seed data for subjects
            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, SubjectName = "Mathematics", ClassId = 1 },
                new Subject { SubjectId = 2, SubjectName = "Science", ClassId = 1 },
                new Subject { SubjectId = 3, SubjectName = "History", ClassId = 2 },
                new Subject { SubjectId = 4, SubjectName = "Geography", ClassId = 2 },
                new Subject { SubjectId = 5, SubjectName = "Literature", ClassId = 3 }
            // Add more subjects as needed
            );

            // Seed data for topics
            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = 1, TopicName = "Algebra", SubjectId = 1 },
                new Topic { TopicId = 2, TopicName = "Geometry", SubjectId = 1 },
                new Topic { TopicId = 3, TopicName = "Physics Basics", SubjectId = 2 },
                new Topic { TopicId = 4, TopicName = "World War I", SubjectId = 3 },
                new Topic { TopicId = 5, TopicName = "Continents and Oceans", SubjectId = 4 }
            // Add more topics as needed
            );

            // Seed data for students
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "Alice", Email = "alice@example.com", PasswordHash = "hashedPassword1", ClassId = 1 },
                new Student { StudentId = 2, Name = "Bob", Email = "bob@example.com", PasswordHash = "hashedPassword2", ClassId = 1 },
                new Student { StudentId = 3, Name = "Charlie", Email = "charlie@example.com", PasswordHash = "hashedPassword3", ClassId = 2 },
                new Student { StudentId = 4, Name = "David", Email = "david@example.com", PasswordHash = "hashedPassword4", ClassId = 2 },
                new Student { StudentId = 5, Name = "Eve", Email = "eve@example.com", PasswordHash = "hashedPassword5", ClassId = 3 }
            // Add more students as needed
            );

            // Seed data for student files
            modelBuilder.Entity<StudentFile>().HasData(
                new StudentFile { FileId = 1, FileName = "Math Homework", TopicId = 1, FilePath = "/" },
                new StudentFile { FileId = 2, FileName = "Science Project", TopicId = 3, FilePath = "/" },
                new StudentFile { FileId = 3, FileName = "History Essay", TopicId = 4, FilePath = "/" }
            // Add more files as needed
            );
            // Seed data for settings
            modelBuilder.Entity<Setting>().HasData(
                new Setting {SettingId=1, Key= "logo-url", Value= "http://placehold.it/259x30" },
                new Setting { SettingId = 2, Key = "email", Value = "support@themerex.net" },
                new Setting { SettingId = 3, Key = "phone", Value = "0 800 123-4567" },
                new Setting { SettingId = 4, Key = "address", Value = "put address here" }
            );

            // Student -> Class (One-to-Many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId);

            // Subject -> Topic (One-to-Many)
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Subject)
                .WithMany(s => s.Topics)
                .HasForeignKey(t => t.SubjectId);

            // Topic -> File (One-to-Many)
            modelBuilder.Entity<StudentFile>()
                .HasOne(f => f.Topic)
                .WithMany(t => t.Files)
                .HasForeignKey(f => f.TopicId);
        }


    }

}
