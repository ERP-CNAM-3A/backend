using Domain.Entities.Users;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Users.Commands
{
    public sealed record CreateUser_Command(
        string FirstName,
        string LastName,
        string Email,
        string Phone
    ) : IRequest<User>;

    internal sealed class CreateUser_CommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUser_Command, User>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> Handle(CreateUser_Command request, CancellationToken cancellationToken)
        {
            User user = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone
            );

            _userRepository.Add(user);

            return user;
        }
    }
}
