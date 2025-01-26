using Domain.Entities.Projects;
using Domain.Enums;

namespace API.DTO.ProjectDTOs
{
    public sealed class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysRequired { get; set; }
        public ProjectStatus Status { get; set; }

        public ProjectDTO() { }

        public ProjectDTO(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            ProjectType = project.ProjectType;
            SaleType = project.SaleType;
            DueDate = project.DueDate;
            DaysRequired = project.DaysRequired;
            Status = project.Status;
        }
    }
}
