namespace Domain.Entities.Users
{
    public sealed class Customer : User
    {
        public Customer(string firstName, string lastName, string email, string phone) : base(firstName, lastName, email, phone)
        {
        }
        public Customer(Guid id, string firstName, string lastName, string email, string phone) : base(id, firstName, lastName, email, phone)
        {
        }
    }
}
