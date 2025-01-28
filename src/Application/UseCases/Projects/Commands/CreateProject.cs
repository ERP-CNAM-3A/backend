using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record CreateProject_Command(
        Sale Sales,
        double Days,
        List<Ressource> Ressources
    ) : IRequest<Project>;

    internal sealed class CreateProject_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProject_Command, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(CreateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = new Project(
                Guid.NewGuid(),
                request.Sales,
                request.Days,
                request.Ressources
            );

            _projectRepository.Add(project);

            return project;
        }
    }
}
