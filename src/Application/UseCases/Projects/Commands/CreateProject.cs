using Domain.Entities.Projects;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record CreateProject_Command(
        string Name,
        ProjectType ProjectType,
        SaleType SaleType,
        DateTime DueDate,
        int DaysRequired,
        ProjectStatus Status
    ) : IRequest<Project>;

    internal sealed class CreateProject_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProject_Command, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(CreateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = new Project(
                Guid.NewGuid(),
                request.Name,
                request.ProjectType,
                request.SaleType,
                request.DueDate,
                request.DaysRequired,
                request.Status
            );

            _projectRepository.Add(project);

            return project;
        }
    }
}
