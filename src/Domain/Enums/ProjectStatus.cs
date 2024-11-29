namespace Domain.Enums
{
    public enum ProjectStatus
    {
        Active,
        Inactive,
        Completed
    }

    public static class ProjectStatusExtensions
    {
        public static string ToFriendlyString(this ProjectStatus status)
        {
            switch (status)
            {
                case ProjectStatus.Active:
                    return "Active";
                case ProjectStatus.Inactive:
                    return "Inactive";
                case ProjectStatus.Completed:
                    return "Completed";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}
