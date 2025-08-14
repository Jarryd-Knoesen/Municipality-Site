using Microsoft.EntityFrameworkCore;
using PROG7312_P1_V1.DataModels;

namespace PROG7312_P1_V1.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<Events> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
