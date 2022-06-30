using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using models.Entities.FitUser;
using Models.Configuration.ConnectionString;

namespace infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        private readonly ConnectionStrings _connectionString;

        public ContextBase(DbContextOptions<ContextBase> options, IOptions<ConnectionStrings> connectionString) : base(options)
        {
            _connectionString = connectionString.Value;
        }

        public DbSet<FitUser> FitUser => Set<FitUser>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetStringConnectionConfig(), ServerVersion.AutoDetect(GetStringConnectionConfig()));
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        private string GetStringConnectionConfig()
        {
            string strCon = _connectionString.FitExerciseDB;
            return strCon;
        }
    }
}