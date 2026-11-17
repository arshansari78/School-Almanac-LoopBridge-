using LoopBridge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBridge.Repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Diary> Diarys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hashed = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "administrator",
                UserName = "administrator",
                NormalizedUserName = "administrator",
                FullName = "Power User",
                Email = "admin@example.com",
                PhoneNumber = "1234567890",
                PasswordHash = hashed.HashPassword(null, "Admin@2020")
            });

            builder.Entity<School>().HasIndex(e=> e.Email).IsUnique(true);

        }
    }
}
