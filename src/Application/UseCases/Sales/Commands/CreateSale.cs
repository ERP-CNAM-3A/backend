using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Commands
{
    public sealed record CreateSale_Command(
        string Type,
        string Client,
        string Product,
        decimal Amount,
        string Date,
        string Status,
        string Probability
    ) : IRequest<Sale>;

    internal sealed class CreateSale_CommandHandler(ISaleRepository saleRepository) : IRequestHandler<CreateSale_Command, Sale>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task<Sale> Handle(CreateSale_Command request, CancellationToken cancellationToken)
        {
            Sale sale = new Sale(
                Guid.NewGuid(),
                request.Type,
                request.Client,
                request.Product,
                request.Amount,
                request.Date,
                request.Status,
                request.Probability
            );

            _saleRepository.Add(sale);

            return sale;
        }
    }
}
