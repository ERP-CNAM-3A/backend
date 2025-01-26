using Domain.Entities.Ressources;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Commands
{
    public sealed record CreateRessource_Command(
        string Name,
        RessourceType Type,
        int DailyRate
    ) : IRequest<Ressource>;

    internal sealed class CreateRessource_CommandHandler(IRessourceRepository ressourceRepository) : IRequestHandler<CreateRessource_Command, Ressource>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task<Ressource> Handle(CreateRessource_Command request, CancellationToken cancellationToken)
        {
            Ressource ressource = new Ressource(
                Guid.NewGuid(),
                request.Name,
                request.Type,
                request.DailyRate
            );

            _ressourceRepository.Add(ressource);

            return ressource;
        }
    }
}
