using Domain.Entities.Users;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Update(User user);
        public void DeleteById(Guid id);
        public User GetById(Guid id);
        public List<User> GetAll();
    }
}
