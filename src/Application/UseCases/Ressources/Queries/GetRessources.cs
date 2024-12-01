using Domain.Repositories;
using MediatR;
using Domain.Entities;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetRessources_Query() : IRequest<List<Ressource>>;

    internal sealed class GetRessources_QueryHandler : IRequestHandler<GetRessources_Query, List<Ressource>>
    {
        private readonly IRessourceRepository _ressourceRepository;

        public GetRessources_QueryHandler(IRessourceRepository ressourceRepository)
        {
            _ressourceRepository = ressourceRepository;
        }

        public Task<List<Ressource>> Handle(GetRessources_Query request, CancellationToken cancellationToken)
        {
            var ressources = _ressourceRepository.GetAll();
            return Task.FromResult(ressources);
        }
    }
}

