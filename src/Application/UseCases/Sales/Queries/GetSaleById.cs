using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Queries
{
    public sealed record GetSaleById_Query(Guid id) : IRequest<Sale>;

    internal sealed class GetSaleById_QueryHandler(ISaleRepository saleRepository) : IRequestHandler<GetSaleById_Query, Sale>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<Sale> Handle(GetSaleById_Query request, CancellationToken cancellationToken)
        {
            return _saleRepository.GetById(request.id);
        }
    }
}
