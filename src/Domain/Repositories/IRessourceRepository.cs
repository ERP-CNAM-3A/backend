using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRessourceRepository
    {
        public Ressource GetById(Guid id);
        public List<Ressource> GetAll();
    }
}
