using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Entities.FitUser;
using Models.Configuration.ConnectionString;
using Models.Entities.Gym;
using Models.Entities.Plan;

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
        public DbSet<GymEntity> Gyms => Set<GymEntity>();
        public DbSet<PlanEntity> Plan => Set<PlanEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetStringConnectionConfig());
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