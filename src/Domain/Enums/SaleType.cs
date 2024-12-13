namespace Domain.Enums
{
    public enum SaleType
    {
        Order,
        Quote,
        Opportunity
    }

    public static class SaleTypeExtensions
    {
        public static string ToFriendlyString(this SaleType type)
        {
            switch (type)
            {
                case SaleType.Order:
                    return "Commande";
                case SaleType.Quote:
                    return "Devis";
                case SaleType.Opportunity:
                    return "Opportunité";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
