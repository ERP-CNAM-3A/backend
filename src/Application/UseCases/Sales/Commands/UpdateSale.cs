using MediatR;
using Domain.Repositories;
using Domain.Enums;
using Domain.Entities.Sales;

namespace Application.UseCases.Sales.Commands
{
    public sealed record UpdateSale_Command(
        Guid Id,
        SaleType Type,
        Guid ClientId,
        string What,
        decimal Quantity,
        SaleStatus Status,
        double Chance
    ) : IRequest<Sale>;

    internal sealed class UpdateSale_CommandHandler(ISaleRepository saleRepository) : IRequestHandler<UpdateSale_Command, Sale>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<Sale> Handle(UpdateSale_Command request, CancellationToken cancellationToken)
        {
            Sale sale = _saleRepository.GetById(request.Id);

            sale.Update(
                request.Type,
                request.ClientId,
                request.What,
                request.Quantity,
                request.Status,
                request.Chance
            );

            _saleRepository.Update(sale);

            return sale;
        }
    }
}
