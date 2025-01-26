using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetAllRessources_Query() : IRequest<List<Ressource>>;

    internal sealed class GetAllRessources_QueryHandler(IRessourceRepository ressourceRepository) : IRequestHandler<GetAllRessources_Query, List<Ressource>>
    {
        private readonly IRessourceRepository _ressourceRepository = ressourceRepository;

        public async Task<List<Ressource>> Handle(GetAllRessources_Query request, CancellationToken cancellationToken)
        {
            return _ressourceRepository.GetAll().ToList();
        }
    }
}
