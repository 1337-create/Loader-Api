using Anubis.Win32.Server.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Anubis.Win32.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Period> Periods { get; set; }

        private readonly string m_Host;
        private readonly string m_User;
        private readonly string m_Password;
        private readonly string m_Database;

        public DatabaseContext(string host, string user, string password, string db)
        {
            m_Host = host;
            m_User = user;
            m_Password = password;
            m_Database = db;
        }

        protected override void OnConfiguring( DbContextOptionsBuilder options )
        {
            options.EnableSensitiveDataLogging();
            options.UseMySql( $"server={m_Host};Userid={m_User};Password={m_Password};database={m_Database};" );
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
        }
    }
}
