using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record AssignRessource_Command(Guid ProjectId, int RessourceId, DateTime From, DateTime To) : IRequest<Project>;

    internal sealed class AssignRessource_CommandHandler : IRequestHandler<AssignRessource_Command, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IExternalRessourceService _externalRessourceService;

        public AssignRessource_CommandHandler(IProjectRepository projectRepository, IExternalRessourceService externalRessourceService)
        {
            _projectRepository = projectRepository;
            _externalRessourceService = externalRessourceService;
        }

        public async Task<Project> Handle(AssignRessource_Command request, CancellationToken cancellationToken)
        {

            // Get the project from the repository
            var project = _projectRepository.GetById(request.ProjectId);
            if (project == null)
            {
                throw new ProjectNotFoundException(request.ProjectId);
            }

            // Calculate the project date range
            var projectStartDate = DateTime.Parse(project.Sale.Date);
            var projectEndDate = projectStartDate.AddDays(project.WorkDaysNeeded);

            // Check if the assignation is between the sale date and the end date
            if (request.From < projectStartDate || request.To > projectEndDate)
            {
                throw new RessourceNotInProjectDateRangeException(request.RessourceId, projectStartDate, projectEndDate);
            }

            // Check if the resource is already assigned to the project for the given period
            if (project.Ressources.Any(r => r.Id == request.RessourceId && r.AvailabilityPeriods.Any(ap => ap.StartDate <= request.From && ap.EndDate >= request.To)))
            {
                throw new RessourceAlreadyAssignedException(request.RessourceId);
            }

            // Call the external API to reserve the resource
            var success = await _externalRessourceService.ReserveRessourceAsync(request.RessourceId, request.From, request.To);
            if (!success)
            {
                throw new RessourceExternalAPIException(request.RessourceId);
            }

            // Create the new resource and add it to the project
            var ressource = new Ressource(request.RessourceId, new List<AvailabilityPeriod> { new AvailabilityPeriod(request.From, request.To) });
            project.Ressources.Add(ressource);

            // Update the project in the repository
            _projectRepository.Update(project);

            return project;
        }
    }
}





