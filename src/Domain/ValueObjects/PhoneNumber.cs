using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public sealed record PhoneNumber
    {
        private static readonly Regex PhoneNumberRegex = new(@"^\d{10}$", RegexOptions.Compiled);

        public string Value { get; }

        public PhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));
            }

            if (!PhoneNumberRegex.IsMatch(phoneNumber))
            {
                throw new ArgumentException("Phone number must contain exactly 10 numeric digits.", nameof(phoneNumber));
            }

            Value = phoneNumber;
        }
    }
}
