using Domain.Entities.Ressources;

namespace API.DTO.RessourceDTOs
{
    public sealed class RessourceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DailyRate { get; set; }

        public RessourceDTO() { }

        public RessourceDTO(Ressource ressource)
        {
            Id = ressource.Id;
            Name = ressource.Name;
            DailyRate = ressource.DailyRate;
        }
    }
}
