using MediatR;
using Domain.Repositories;
using Domain.Exceptions;
using Domain.Entities.Users;

namespace Application.UseCases.Users.Commands
{
    public sealed record DeleteUser_Command(Guid id) : IRequest;

    internal sealed class DeleteUser_CommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUser_Command>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Handle(DeleteUser_Command request, CancellationToken cancellationToken)
        {
            _userRepository.DeleteById(request.id);
        }
    }
}