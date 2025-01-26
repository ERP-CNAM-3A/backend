using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities.Projects
{
    public sealed class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ProjectType ProjectType { get; private set; }
        public SaleType SaleType { get; private set; }
        public DateTime DueDate { get; private set; }
        public int DaysRequired { get; private set; }
        public ProjectStatus Status { get; private set; }

        [JsonConstructor]
        public Project(Guid id, string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status)
        {
            Id = id;
            Name = name;
            ProjectType = projectType;
            SaleType = saleType;
            DueDate = dueDate;
            DaysRequired = daysRequired;
            Status = status;
        }

        public Project(string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status)
        {
            Name = name;
            ProjectType = projectType;
            SaleType = saleType;
            DueDate = dueDate;
            DaysRequired = daysRequired;
            Status = status;
        }

        public void Update(string name, ProjectType projectType, SaleType saleType, DateTime dueDate, int daysRequired, ProjectStatus status)
        {
            Name = name;
            ProjectType = projectType;
            SaleType = saleType;
            DueDate = dueDate;
            DaysRequired = daysRequired;
            Status = status;
        }
    }
}
