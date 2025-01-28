using Domain.Entities.Projects;
using Domain.Entities.Ressources;

namespace API.DTO.ProjectDTOs
{
    public sealed class UpdateProjectDTO
    {
        public double WorkDays { get; set; }
        public List<Ressource> Ressources { get; set; }

        public UpdateProjectDTO() { }

        public UpdateProjectDTO(Project project)
        {
            WorkDays = project.WorkDays;
            Ressources = project.Ressources;
        }
    }
}
