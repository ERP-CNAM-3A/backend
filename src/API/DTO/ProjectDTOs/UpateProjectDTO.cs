using Domain.Entities.Projects;
using Domain.Enums;

namespace API.DTO.ProjectDTOs
{
    public sealed class UpdateProjectDTO
    {
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysRequired { get; set; }
        public ProjectStatus Status { get; set; }

        public UpdateProjectDTO() { }

        public UpdateProjectDTO(Project project)
        {
            Name = project.Name;
            ProjectType = project.ProjectType;
            SaleType = project.SaleType;
            DueDate = project.DueDate;
            DaysRequired = project.DaysRequired;
            Status = project.Status;
        }
    }
}
