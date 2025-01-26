using Domain.Entities.Users;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Users.Queries
{
    public sealed record GetAllUsers_Query() : IRequest<List<User>>;

    internal sealed class GetAllUsers_QueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsers_Query, List<User>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<List<User>> Handle(GetAllUsers_Query request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll().ToList();
        }
    }
}
