using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Ressources.Queries
{
    public sealed record GetRessourceById_Query(Guid Id) : IRequest<Ressource>;

    internal sealed class GetRessourceById_QueryHandler : IRequestHandler<GetRessourceById_Query, Ressource>
    {
        private readonly IRessourceRepository _ressourceRepository;
        public GetRessourceById_QueryHandler(IRessourceRepository ressourceRepository)
        {
            _ressourceRepository = ressourceRepository;
        }
        public Task<Ressource> Handle(GetRessourceById_Query request, CancellationToken cancellationToken)
        {
            var ressource = _ressourceRepository.GetById(request.Id);
            return Task.FromResult(ressource);
        }
    }
}
