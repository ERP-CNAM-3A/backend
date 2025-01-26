using MediatR;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.Exceptions;

namespace Application.UseCases.Users.Commands
{
    public sealed record UpdateUser_Command(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Phone
    ) : IRequest<User>;

    internal sealed class UpdateUser_CommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUser_Command, User>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> Handle(UpdateUser_Command request, CancellationToken cancellationToken)
        {
            User user = _userRepository.GetById(request.Id);

            user.Update(request.FirstName, request.LastName, request.Email, request.Phone);

            _userRepository.Update(user);

            return user;
        }
    }
}
