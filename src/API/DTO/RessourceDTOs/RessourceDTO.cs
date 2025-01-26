using Domain.Entities.Ressources;
using Domain.Enums;

namespace API.DTO.RessourceDTOs
{
    public sealed class RessourceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RessourceType Type { get; set; }
        public int DailyRate { get; set; }

        public RessourceDTO() { }

        public RessourceDTO(Ressource ressource)
        {
            Id = ressource.Id;
            Name = ressource.Name;
            Type = ressource.Type;
            DailyRate = ressource.DailyRate;
        }
    }
}
