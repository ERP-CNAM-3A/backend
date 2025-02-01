using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using System.Text.Json.Serialization;

namespace Domain.Entities.Projects
{
    public sealed class Project
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

        public Project() { }

        [JsonConstructor]
        public Project(Guid id, Sale sale, double workDaysNeeded, List<Ressource> ressources)
        {
            Id = id;
            Sale = sale;
            WorkDaysNeeded = workDaysNeeded;
            Ressources = ressources;
        }

        public Project(Sale sale, double workDaysNeeded, List<Ressource> ressources)
        {
            Id = Guid.NewGuid();
            Sale = sale;
            WorkDaysNeeded = workDaysNeeded;
            Ressources = ressources;
        }

        public void Update(double workDaysNeeded)
        {
            WorkDaysNeeded = workDaysNeeded;
        }
    }
}
