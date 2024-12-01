using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    internal class RessourceRepository : IRessourceRepository
    {
        private readonly List<Ressource> _ressources = new List<Ressource>
        {
            new Ressource(new Guid("ccc28a5a-c1ac-48c2-915e-f12fe3ef9b26"), "Isamettin AYDIN", RessourceType.Human, 100),
            new Ressource(new Guid("ccc28a5a-c1ac-48c2-915e-f12fe3ef9b16"), "Serveur", RessourceType.Material, 200)
        };

        public List<Ressource> GetAll()
        {
            return _ressources;
        }

        public Ressource GetById(Guid id)
        {
            var ressource = _ressources.FirstOrDefault(r => r.Id == id);
            if (ressource == null)
            {
                throw new Exception("Ressource not found");
            }
            return ressource;
        }
    }
}

