namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid id) 
            : base($"User with ID {id} not found.") { }
    }

    public class UserSaveException : Exception
    {
        public UserSaveException() : base("An error occurred while saving the user data.") { }
    }

    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(Guid id) 
            : base($"User with ID {id} already exists.") { }
    }
}
