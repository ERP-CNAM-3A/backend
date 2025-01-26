using Domain.Entities.Projects;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Queries
{
    public sealed record GetAllProjects_Query() : IRequest<List<Project>>;

    internal sealed class GetAllProjects_QueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjects_Query, List<Project>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<List<Project>> Handle(GetAllProjects_Query request, CancellationToken cancellationToken)
        {
            return _projectRepository.GetAll().ToList();
        }
    }
}
