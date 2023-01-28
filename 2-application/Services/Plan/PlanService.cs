using System;
using Application.Interfaces.Plan;
using Infrastructure.Repository.Interfaces.Plan;
using Models.Dto.Plan;

namespace Application.Services.Plan
{
	public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<List<PlanDTO>> GetPlans()
        {
            return await _planRepository.GetPlans();
        }
    }
}

