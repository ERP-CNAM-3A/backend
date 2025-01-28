using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Commands
{
    public sealed record CreateRessource_Command(
        DateTime From,
        DateTime To
    ) : IRequest<Ressource>;

    internal sealed class CreateRessource_CommandHandler(IRessourceRepository ressourceRepository) : IRequestHandler<CreateRessource_Command, Ressource>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task<Ressource> Handle(CreateRessource_Command request, CancellationToken cancellationToken)
        {
            Ressource ressource = new Ressource(
                Guid.NewGuid(),
                request.From,
                request.To
            );

            _ressourceRepository.Add(ressource);

            return ressource;
        }
    }
}
