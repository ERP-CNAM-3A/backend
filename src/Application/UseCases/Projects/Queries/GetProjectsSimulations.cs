using Domain.Entities.Projects;
using Domain.Repositories;
using Application.Models;
using MediatR;

namespace Application.UseCases.Projects.Queries
{
    public sealed record GetProjectsSimulations_Query() : IRequest<List<ProjectSimulationResult>>;

    internal sealed class GetProjectsSimulations_QueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectsSimulations_Query, List<ProjectSimulationResult>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<List<ProjectSimulationResult>> Handle(GetProjectsSimulations_Query request, CancellationToken cancellationToken)
        {
            List<Project> projects = _projectRepository.GetAll();

            List<ProjectSimulationResult> results = projects.Select(project => new ProjectSimulationResult
            {
                ProjectId = project.Id,
                WorkDays = project.WorkDaysNeeded,
                RessourcesAssigned = project.Ressources.Count,
                AvailableWorkDays = project.Ressources.Sum(r => r.DaysWorking),
                RemainingNeededWorkDays = project.WorkDaysNeeded - project.Ressources.Sum(r => r.DaysWorking),
                CanBeDeliveredOnTime = project.Ressources.Sum(r => r.DaysWorking) >= project.WorkDaysNeeded
            }).ToList();

            return await Task.FromResult(results);
        }
    }
}
