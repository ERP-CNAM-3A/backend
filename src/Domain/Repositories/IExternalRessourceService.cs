using Domain.Entities.Ressources;

namespace Domain.Repositories
{
    public interface IExternalRessourceService
    {
        public Task<List<Ressource>> GetAllRessourcesAsync();
        public Task<List<Ressource>> GetAvailableRessourcesBetweenAsync(DateTime startDate, DateTime endDate);
        public Task<bool> ReserveRessourceAsync(int ressourceId, DateTime from, DateTime to);

        public Task<bool> CancelReservationAsync(int ressourceId, DateTime from, DateTime to);
    }
}
