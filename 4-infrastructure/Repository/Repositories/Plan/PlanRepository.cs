using System;
using infrastructure.Configuration;
using Infrastructure.Repository.Interfaces.Plan;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Configuration.ConnectionString;
using Models.Dto.Plan;

namespace Infrastructure.Repository.Repositories.Plan
{
	public class PlanRepository : IPlanRepository
	{
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<ConnectionStrings> _connectionString;

        public PlanRepository(IOptions<ConnectionStrings> connectionString)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _connectionString = connectionString;
        }

        public async Task<List<PlanDTO>> GetPlans()
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var plans = await context.Plan.Select(x => new PlanDTO(x)).AsNoTracking().ToListAsync();

            return plans;
        }
    }
}