using FSTW_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace FSTW_backend
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Achievement> Achievement { get; set; } = null!;
        public DbSet<Contact> Contact { get; set; } = null!;
        public DbSet<Education> Education { get; set; } = null!;
        public DbSet<Expirience> Expirience { get; set; } = null!;
        public DbSet<Favorite> Favorite { get; set; } = null!;
        public DbSet<Internship> Internship { get; set; } = null!;
        public DbSet<Link> Link { get; set; } = null!;
        public DbSet<Profile> Profile { get; set; } = null!;
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Requirement> Requirement { get; set; } = null!;
        public DbSet<Resume> Resume { get; set; } = null!;
        public DbSet<Skill> Skill { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
