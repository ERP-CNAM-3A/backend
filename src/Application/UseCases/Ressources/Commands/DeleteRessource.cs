using MediatR;
using Domain.Repositories;

namespace Application.UseCases.Ressources.Commands
{
    public sealed record DeleteRessource_Command(Guid Id) : IRequest;

    internal sealed class DeleteRessource_CommandHandler(IRessourceRepository ressourceRepository) : IRequestHandler<DeleteRessource_Command>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task Handle(DeleteRessource_Command request, CancellationToken cancellationToken)
        {
            _ressourceRepository.DeleteById(request.Id);
        }
    }
}