using Domain.Entities.Ressources;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetAvailableRessourcesBetween_Query(DateTime StartDate, DateTime EndDate) : IRequest<List<Ressource>>;

    internal sealed class GetAvailableRessourcesBetween_QueryHandler : IRequestHandler<GetAvailableRessourcesBetween_Query, List<Ressource>>
    {
        private readonly IExternalRessourceService _externalRessourceService;

        public GetAvailableRessourcesBetween_QueryHandler(IExternalRessourceService externalRessourceService)
        {
            _externalRessourceService = externalRessourceService;
        }

        public async Task<List<Ressource>> Handle(GetAvailableRessourcesBetween_Query request, CancellationToken cancellationToken)
        {
            return await _externalRessourceService.GetAvailableRessourcesBetweenAsync(request.StartDate, request.EndDate);
        }
    }
}
