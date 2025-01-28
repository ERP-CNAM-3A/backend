using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Commands
{
    public sealed record UpdateSale_Command(
        Guid Id,
        string Type,
        string Client,
        string Product,
        decimal Amount,
        string Date,
        string Status,
        string Probability
    ) : IRequest<Sale>;

    internal sealed class UpdateSale_CommandHandler(ISaleRepository saleRepository) : IRequestHandler<UpdateSale_Command, Sale>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<Sale> Handle(UpdateSale_Command request, CancellationToken cancellationToken)
        {
            Sale sale = _saleRepository.GetById(request.Id);

            sale.Update(
                request.Type,
                request.Client,
                request.Product,
                request.Amount,
                request.Date,
                request.Status,
                request.Probability
            );

            _saleRepository.Update(sale);

            return sale;
        }
    }
}
