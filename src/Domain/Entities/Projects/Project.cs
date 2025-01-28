using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using System.Text.Json.Serialization;

namespace Domain.Entities.Projects
{
    public sealed class Project
    {
        public Guid Id { get; set; }
        public Sale Sale { get; set; }
        public double Days { get; set; }
        public List<Ressource> Ressources { get; set; }

        public Project() { }

        [JsonConstructor]
        public Project(Guid id, Sale sale, double days, List<Ressource> ressources)
        {
            Id = id;
            Sale = sale;
            Days = days;
            Ressources = ressources;
        }

        public Project(Sale sale, double days, List<Ressource> ressources)
        {
            Id = Guid.NewGuid();
            Sale = sale;
            Days = days;
            Ressources = ressources;
        }

        public void Update(double days, List<Ressource> ressources)
        {
            Days = days;
            Ressources = ressources;
        }
    }
}
