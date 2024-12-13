namespace Domain.Enums
{
    public enum ProjectType
    {
        ProjectManagement,
        Development
    }

    public static class ProjectTypeExtensions
    {
        public static string ToFriendlyString(this ProjectType type)
        {
            switch (type)
            {
                case ProjectType.ProjectManagement:
                    return "Gestion de projet";
                case ProjectType.Development:
                    return "Développement";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
