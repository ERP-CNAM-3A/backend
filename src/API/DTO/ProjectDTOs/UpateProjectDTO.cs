using Domain.Entities.Projects;
using Domain.Entities.Ressources;

namespace API.DTO.ProjectDTOs
{
    public sealed class UpdateProjectDTO
    {
        public double Days { get; set; }
        public List<Ressource> Ressources { get; set; }

        public UpdateProjectDTO() { }

        public UpdateProjectDTO(Project project)
        {
            Days = project.Days;
            Ressources = project.Ressources;
        }
    }
}
