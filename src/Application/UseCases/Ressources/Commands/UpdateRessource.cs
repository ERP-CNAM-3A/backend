using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Commands
{
    public sealed record UpdateRessource_Command(
        Guid Id,
        DateTime From,
        DateTime To
    ) : IRequest<Ressource>;

    internal sealed class UpdateRessource_CommandHandler(IRessourceRepository ressourceRepository) : IRequestHandler<UpdateRessource_Command, Ressource>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task<Ressource> Handle(UpdateRessource_Command request, CancellationToken cancellationToken)
        {
            Ressource ressource = _ressourceRepository.GetById(request.Id);

            ressource.Update(request.From, request.To);

            _ressourceRepository.Update(ressource);

            return ressource;
        }
    }
}