using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRessourceRepository
    {
        public void Add(Ressource ressource);
        public void Update(Ressource ressource);
        public void DeleteById(Guid id);
        public Ressource GetById(Guid id);
        public List<Ressource> GetAll();
    }
}
