using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using System.Text.Json.Serialization;

namespace Domain.Entities.Projects
{
    public sealed class Project
    {
        public Guid Id { get; set; }
        public Sale Sale { get; set; }
        public double WorkDays { get; set; }
        public List<Ressource> Ressources { get; set; }

        public Project() { }

        [JsonConstructor]
        public Project(Guid id, Sale sale, double workDays, List<Ressource> ressources)
        {
            Id = id;
            Sale = sale;
            WorkDays = workDays;
            Ressources = ressources;
        }

        public Project(Sale sale, double workDays, List<Ressource> ressources)
        {
            Id = Guid.NewGuid();
            Sale = sale;
            WorkDays = workDays;
            Ressources = ressources;
        }

        public void Update(double workDays, List<Ressource> ressources)
        {
            WorkDays = workDays;
            Ressources = ressources;
        }
    }
}
