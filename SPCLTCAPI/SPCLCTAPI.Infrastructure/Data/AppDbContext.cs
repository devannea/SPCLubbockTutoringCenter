using Microsoft.EntityFrameworkCore;
using SPCLCTAPI.Core.Models;

namespace SPCLCTAPI.Infrastructure.Data
{
    class AppDbContext : IndentityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=../SPCLCTAPI.Infrastructure/profile.db");
        }
    }
}
