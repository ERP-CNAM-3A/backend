using Domain.Entities.Projects;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record DeassignRessource_Command(Guid ProjectId, int RessourceId, DateTime From, DateTime To) : IRequest<Project>;

    internal sealed class DeassignRessource_CommandHandler : IRequestHandler<DeassignRessource_Command, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IExternalRessourceService _externalRessourceService;

        public DeassignRessource_CommandHandler(IProjectRepository projectRepository, IExternalRessourceService externalRessourceService)
        {
            _projectRepository = projectRepository;
            _externalRessourceService = externalRessourceService;
        }

        public async Task<Project> Handle(DeassignRessource_Command request, CancellationToken cancellationToken)
        {
            // Get the project from the repository
            var project = _projectRepository.GetById(request.ProjectId);
            if (project == null)
            {
                throw new ProjectNotFoundException(request.ProjectId);
            }

            // Find the resource in the project
            var ressource = project.Ressources.FirstOrDefault(r => r.Id == request.RessourceId);
            if (ressource == null)
            {
                throw new RessourceNotFoundException(request.RessourceId);
            }

            // Find the availability period to remove
            var periodToRemove = ressource.AvailabilityPeriods.FirstOrDefault(ap => ap.StartDate == request.From && ap.EndDate == request.To);
            if (periodToRemove == null)
            {
                throw new RessourceNotAssignedException(request.RessourceId);
            }

            // Call the external API to dereserve the resource
            var success = await _externalRessourceService.CancelReservationAsync(request.RessourceId, request.From, request.To);
            if (!success)
            {
                throw new RessourceExternalAPIException(request.RessourceId);
            }

            // Remove the availability period
            ressource.AvailabilityPeriods.Remove(periodToRemove);

            // If the resource has no more availability periods, remove the resource from the project
            if (!ressource.AvailabilityPeriods.Any())
            {
                project.Ressources.Remove(ressource);
            }

            // Update the project in the repository
            _projectRepository.Update(project);

            return project;
        }
    }
}




