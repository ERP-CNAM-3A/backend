using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Projects.Commands
{
    public sealed record DeleteProject_Command(Guid id) : IRequest;

    internal sealed class DeleteUser_CommandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProject_Command>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task Handle(DeleteProject_Command request, CancellationToken cancellationToken)
        {
            _projectRepository.DeleteById(request.id);
        }
    }
}