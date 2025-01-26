using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private const string FileName = "users.json";

        public void Add(User user)
        {
            try
            {
                var users = JsonFileHandler.ReadFromFile<User>(FileName);

                if (users.Any(u => u.Id == user.Id))
                {
                    throw new UserAlreadyExistsException(user.Id);
                }

                users.Add(user);
                JsonFileHandler.WriteToFile(FileName, users);
            }
            catch (Exception)
            {
                throw new UserSaveException();
            }
        }

        public void DeleteById(Guid id)
        {
            try
            {
                var users = JsonFileHandler.ReadFromFile<User>(FileName);
                var userToRemove = users.FirstOrDefault(u => u.Id == id);

                if (userToRemove == null)
                {
                    throw new UserNotFoundException(id);
                }

                users.Remove(userToRemove);
                JsonFileHandler.WriteToFile(FileName, users);
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UserSaveException();
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return JsonFileHandler.ReadFromFile<User>(FileName);
            }
            catch (Exception)
            {
                throw new UserSaveException();
            }
        }

        public User GetById(Guid id)
        {
            try
            {
                var users = JsonFileHandler.ReadFromFile<User>(FileName);
                var user = users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    throw new UserNotFoundException(id);
                }

                return user;
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UserSaveException();
            }
        }

        public void Update(User user)
        {
            try
            {
                var users = JsonFileHandler.ReadFromFile<User>(FileName);
                var index = users.FindIndex(u => u.Id == user.Id);

                if (index == -1)
                {
                    throw new UserNotFoundException(user.Id);
                }

                users[index] = user;
                JsonFileHandler.WriteToFile(FileName, users);
            }
            catch (UserNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UserSaveException();
            }
        }
    }
}
