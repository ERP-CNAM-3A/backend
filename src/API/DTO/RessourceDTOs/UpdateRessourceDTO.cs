using Domain.Entities.Ressources;

namespace API.DTO.RessourceDTOs
{
    public sealed class UpdateRessourceDTO
    {
        public string Name { get; set; }
        public int DailyRate { get; set; }

        public UpdateRessourceDTO() { }

        public UpdateRessourceDTO(Ressource ressource)
        {
            Name = ressource.Name;
            DailyRate = ressource.DailyRate;
        }
    }
}
