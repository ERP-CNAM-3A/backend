using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Queries
{
    public sealed record GetAllSales_Query() : IRequest<List<Sale>>;

    internal sealed class GetAllSales_QueryHandler(ISaleRepository saleRepository) : IRequestHandler<GetAllSales_Query, List<Sale>>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<List<Sale>> Handle(GetAllSales_Query request, CancellationToken cancellationToken)
        {
            return _saleRepository.GetAll().ToList();
        }
    }
}
