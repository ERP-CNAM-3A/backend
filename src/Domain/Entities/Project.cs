using Domain.Enums;

namespace Domain.Entities
{
    public sealed class Project
    {
        public Guid Id { get; }
        public string Name { get; }
        public ProjectType ProjectType { get; }
        public SaleType SaleType { get; }
        public DateTime DueDate { get; }
        public int DaysRequired { get; }
        public ProjectStatus Status { get; }
        public Ressource[] Ressources { get; }

        public Project(Guid id, string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status, Ressource[] ressources)
        {
            Id = id;
            Name = name;
            ProjectType = projectType;
            SaleType = saleType;
            DueDate = dueDate;
            DaysRequired = daysRequired;
            Status = status;
            Ressources = ressources;
        }

        public Project(string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status, Ressource[] ressources)
        {
            Name = name;
            ProjectType = projectType;
            SaleType = saleType;
            DueDate = dueDate;
            DaysRequired = daysRequired;
            Status = status;
            Ressources = ressources;
        }
    }
}
