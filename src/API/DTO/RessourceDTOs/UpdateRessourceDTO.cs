using Domain.Entities.Ressources;
using Domain.Enums;

namespace API.DTO.RessourceDTOs
{
    public sealed class UpdateRessourceDTO
    {
        public string Name { get; set; }
        public RessourceType Type { get; set; }
        public int DailyRate { get; set; }

        public UpdateRessourceDTO() { }

        public UpdateRessourceDTO(Ressource ressource)
        {
            Name = ressource.Name;
            Type = ressource.Type;
            DailyRate = ressource.DailyRate;
        }
    }
}
