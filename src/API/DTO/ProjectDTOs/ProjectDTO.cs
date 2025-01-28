using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Entities.Sales;

namespace API.DTO.ProjectDTOs
{
    public sealed class ProjectDTO
    {
        public Guid Id { get; set; }
        public Sale Sale { get; set; }
        public double WorkDaysNeeded { get; set; }
        public List<Ressource> Ressources { get; set; }
        public double WorkDaysAvailable
        {
            get => Ressources.Sum(r => r.DaysWorking);
        }
        public bool CanDeliver
        {
            get => WorkDaysAvailable >= WorkDaysNeeded;
        }

        public ProjectDTO() { }

        public ProjectDTO(Project project)
        {
            Id = project.Id;
            Sale = project.Sale;
            WorkDaysNeeded = project.WorkDaysNeeded;
            Ressources = project.Ressources;
        }
    }
}
