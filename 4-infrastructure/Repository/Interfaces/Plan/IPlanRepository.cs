using System;
using Models.Dto.Plan;

namespace Infrastructure.Repository.Interfaces.Plan
{
	public interface IPlanRepository
	{
		Task<List<PlanDTO>> GetPlans();
	}
}

