using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record UpdateProject_Command(
        Guid Id,
        double WorkDays,
        List<Ressource> Ressources
    ) : IRequest<Project>;

    internal sealed class UpdateUser_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProject_Command, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(UpdateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = _projectRepository.GetById(request.Id);

            project.Update(
                request.WorkDays,
                request.Ressources);

            _projectRepository.Update(project);

            return project;
        }
    }
}
