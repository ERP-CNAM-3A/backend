using Domain.Entities.Projects;
using Domain.Entities.Ressources;

namespace API.DTO.ProjectDTOs
{
    public sealed class UpdateProjectDTO
    {
        public double WorkDaysNeeded { get; set; }

        public UpdateProjectDTO() { }

        public UpdateProjectDTO(Project project)
        {
            WorkDaysNeeded = project.WorkDaysNeeded;
        }
    }
}
