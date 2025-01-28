using Domain.Entities.Ressources;

namespace API.DTO.RessourceDTOs
{
    public sealed class RessourceDTO
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int DaysWorking
        {

            get => (To - From).Days;
        }
        public RessourceDTO() { }

        public RessourceDTO(Ressource ressource)
        {
            Id = ressource.Id;
            From = ressource.From;
            To = ressource.To;
        }
    }
}
