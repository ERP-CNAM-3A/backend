using Domain.Entities.Projects;
using Domain.Repositories;
using Application.Models;
using MediatR;

namespace Application.UseCases.Projects.Queries
{
    public sealed record GetProjectSimulationById_Query(Guid ProjectId) : IRequest<ProjectSimulationResult>;

    internal sealed class GetProjectSimulationById_QueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectSimulationById_Query, ProjectSimulationResult>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<ProjectSimulationResult> Handle(GetProjectSimulationById_Query request, CancellationToken cancellationToken)
        {
            var project = _projectRepository.GetById(request.ProjectId);

            var canBeDeliveredOnTime = project.Ressources.Count * project.WorkDaysNeeded >= project.WorkDaysNeeded;

            return new ProjectSimulationResult
            {
                ProjectId = project.Id,
                WorkDaysNeeded = project.WorkDaysNeeded,
                RessourcesAssigned = project.Ressources.Count,
                AvailableWorkDays = project.Ressources.Sum(r => r.DaysWorking),
                RemainingNeededWorkDays = project.WorkDaysNeeded - project.Ressources.Sum(r => r.DaysWorking),
                CanBeDeliveredOnTime = canBeDeliveredOnTime
            };
        }
    }

}
