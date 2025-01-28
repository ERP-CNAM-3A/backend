using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Sales.Commands
{
    public sealed record DeleteSale_Command(Guid id) : IRequest;

    internal sealed class DeleteSale_CommandHandler(ISaleRepository saleRepository) : IRequestHandler<DeleteSale_Command>
    {
        private readonly ISaleRepository _saleRepository = saleRepository;

        public async Task Handle(DeleteSale_Command request, CancellationToken cancellationToken)
        {
            _saleRepository.DeleteById(request.id);
        }
    }
}