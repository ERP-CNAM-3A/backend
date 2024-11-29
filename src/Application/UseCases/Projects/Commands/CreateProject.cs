using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record CreateProject_Command(string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status, Ressource[] ressources) : IRequest<Project>;

    public sealed class CreateProject_CommandHandler(IRessourceRepository ressourceRepository, IProjectRepository projectRepository) : IRequestHandler<CreateProject_Command, Project>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository; // will need to use this later but j'ai trop la flemme là
        private readonly IProjectRepository _projectRepository = projectRepository;
        public Task<Project> Handle(CreateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = new Project(
                request.name,
                request.projectType,
                request.saleType,
                request.dueDate,
                request.daysRequired,
                request.status,
                request.ressources
            );

            _projectRepository.Add(project);
            return Task.FromResult(project);
        }
    }
}
