using Domain.Entities.Users;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public sealed record GetUserById_Query(Guid id) : IRequest<User>;

    internal sealed class GetUserById_QueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserById_Query, User>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> Handle(GetUserById_Query request, CancellationToken cancellationToken)
        {
            return _userRepository.GetById(request.id);
        }
    }
}
