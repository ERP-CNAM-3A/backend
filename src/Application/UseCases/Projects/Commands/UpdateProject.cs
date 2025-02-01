using Domain.Entities.Projects;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record UpdateProject_Command(
        Guid Id,
        double WorkDays
    ) : IRequest<Project>;

    internal sealed class UpdateUser_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProject_Command, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(UpdateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = _projectRepository.GetById(request.Id);

            double totalDaysWorking = project.Ressources.Sum(r => r.DaysWorking);
            if (totalDaysWorking > request.WorkDays)
            {
                throw new Domain.Exceptions.ProjectUpdateWorkDaysException(request.WorkDays, totalDaysWorking);
            }

            project.Update(request.WorkDays);

            _projectRepository.Update(project);

            return project;
        }
    }
}
