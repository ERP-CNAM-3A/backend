using Domain.Entities.Ressources;

namespace API.DTO.RessourceDTOs
{
    public sealed class UpdateRessourceDTO
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public UpdateRessourceDTO() { }

        public UpdateRessourceDTO(Ressource ressource)
        {
            From = ressource.From;
            To = ressource.To;
        }
    }
}
