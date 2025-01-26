using Domain.Entities.Sales;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Commands
{
    public sealed record CreateSale_Command(
        SaleType Type,
        Guid ClientId,
        string What,
        decimal Quantity,
        SaleStatus Status,
        double Chance
    ) : IRequest<Sale>;

    internal sealed class CreateSale_CommandHandler(ISaleRepository saleRepository) : IRequestHandler<CreateSale_Command, Sale>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<Sale> Handle(CreateSale_Command request, CancellationToken cancellationToken)
        {
            Sale sale = new Sale(
                Guid.NewGuid(),
                request.Type,
                request.ClientId,
                request.What,
                request.Quantity,
                request.Status,
                request.Chance
            );

            _saleRepository.Add(sale);

            return sale;
        }
    }
}
