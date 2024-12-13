using Domain.ValueObjects;

namespace Domain.Entities.Users
{
    public abstract class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public User(string firstName, string lastName, string email, string phone) 
        {
            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            Phone = new PhoneNumber(phone);
        }

        public User(Guid id, string firstName, string lastName, string email, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            Phone = new PhoneNumber(phone);
        }
    }
}
