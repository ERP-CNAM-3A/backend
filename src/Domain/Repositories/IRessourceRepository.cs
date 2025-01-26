using Domain.Entities.Ressources;

namespace Domain.Repositories
{
    public interface IRessourceRepository
    {
        public Ressource GetById(Guid id);
        public List<Ressource> GetAll();
        public void Add(Ressource ressource);
        public void Update(Ressource ressource);
        public void DeleteById(Guid id);
    }
}
