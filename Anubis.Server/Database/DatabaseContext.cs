using Anubis.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Period> Periods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();
            //options.UseMySql("server=localhost;UserId=root;Password=;database=xerenity;");
            options.UseMySql("server=194.87.96.24;Userid=root;Password=;database=rust-anubis;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
