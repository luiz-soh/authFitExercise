using Microsoft.EntityFrameworkCore;

namespace infrastructure.configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(GetStringConnectionConfig()));
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        private string GetStringConnectionConfig()
        {
            string strCon = "Server=db-fitexercise.cluster-clvhebcrqq9m.us-east-1.rds.amazonaws.com;Database=fitexercise;Uid=admin;Pwd=SKVbEGLzInMw;";
            return strCon;
        }
    }
}