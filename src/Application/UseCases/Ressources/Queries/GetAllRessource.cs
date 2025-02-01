using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetAllRessources_Query() : IRequest<List<Ressource>>;

    internal sealed class GetAllRessources_QueryHandler : IRequestHandler<GetAllRessources_Query, List<Ressource>>
    {
        private readonly IExternalRessourceService _externalRessourceService;

        public GetAllRessources_QueryHandler(IExternalRessourceService externalRessourceService)
        {
            _externalRessourceService = externalRessourceService;
        }

        public async Task<List<Ressource>> Handle(GetAllRessources_Query request, CancellationToken cancellationToken)
        {
            return await _externalRessourceService.GetAllRessourcesAsync();
        }
    }
}
