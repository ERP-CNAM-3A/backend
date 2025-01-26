using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetRessourceById_Query(Guid id) : IRequest<Ressource>;

    internal sealed class GetRessourceById_QueryHandler(IRessourceRepository ressourceRepository) : IRequestHandler<GetRessourceById_Query, Ressource>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task<Ressource> Handle(GetRessourceById_Query request, CancellationToken cancellationToken)
        {
            return _ressourceRepository.GetById(request.id);
        }
    }
}
