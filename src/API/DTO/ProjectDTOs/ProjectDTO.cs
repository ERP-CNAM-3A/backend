using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Entities.Sales;

namespace API.DTO.ProjectDTOs
{
    public sealed class ProjectDTO
    {
        public Guid Id { get; set; }
        public Sale Sale { get; set; }
        public double WorkDays { get; set; }
        public List<Ressource> Ressources { get; set; }

        public ProjectDTO() { }

        public ProjectDTO(Project project)
        {
            Id = project.Id;
            Sale = project.Sale;
            WorkDays = project.WorkDays;
            Ressources = project.Ressources;
        }
    }
}
