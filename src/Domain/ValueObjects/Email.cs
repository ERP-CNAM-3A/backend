namespace Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email is invalid", nameof(email));
            }
            Value = email;
        }
    }
}
