using MediatR;
using Domain.Repositories;
using Domain.Entities.Projects;
using Domain.Enums;

namespace Application.UseCases.Projects.Commands
{
    public sealed record UpdateProject_Command(
        Guid Id,
        string Name,
        ProjectType ProjectType,
        SaleType SaleType,
        DateTime DueDate,
        int DaysRequired,
        ProjectStatus Status
    ) : IRequest<Project>;

    internal sealed class UpdateUser_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProject_Command, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(UpdateProject_Command request, CancellationToken cancellationToken)
        {
            Project project = _projectRepository.GetById(request.Id);

            project.Update(request.Name,
                request.ProjectType,
                request.SaleType,
                request.DueDate,
                request.DaysRequired,
                request.Status);

            _projectRepository.Update(project);

            return project;
        }
    }
}
