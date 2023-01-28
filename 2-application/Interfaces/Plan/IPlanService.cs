using System;
using Models.Dto.Plan;

namespace Application.Interfaces.Plan
{
	public interface IPlanService
	{
		Task<List<PlanDTO>> GetPlans();
	}
}