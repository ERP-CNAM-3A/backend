using Domain.Entities.Projects;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Queries
{
    public sealed record GetProjectById_Query(Guid id) : IRequest<Project>;

    internal sealed class GetProjectById_QueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectById_Query, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project> Handle(GetProjectById_Query request, CancellationToken cancellationToken)
        {
            return _projectRepository.GetById(request.id);
        }
    }
}
