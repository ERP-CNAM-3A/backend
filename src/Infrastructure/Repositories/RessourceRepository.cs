using Domain.Entities.Ressources;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories
{
    internal sealed class RessourceRepository : IRessourceRepository
    {
        private const string FileName = "ressources.json";  // Le fichier contenant les ressources

        // Ajoute une ressource
        public void Add(Ressource ressource)
        {
            try
            {
                var ressources = JsonFileHandler.ReadFromFile<Ressource>(FileName);

                if (ressources.Any(r => r.Id == ressource.Id))  // Vérifie si la ressource existe déjà
                {
                    throw new RessourceAlreadyExistsException(ressource.Id);
                }

                ressources.Add(ressource);  // Ajoute la nouvelle ressource
                JsonFileHandler.WriteToFile(FileName, ressources);  // Sauvegarde dans le fichier
            }
            catch (Exception)
            {
                throw new RessourceSaveException();
            }
        }

        // Supprime une ressource par son identifiant
        public void DeleteById(Guid id)
        {
            try
            {
                var ressources = JsonFileHandler.ReadFromFile<Ressource>(FileName);
                var ressourceToRemove = ressources.FirstOrDefault(r => r.Id == id);

                if (ressourceToRemove == null)
                {
                    throw new RessourceNotFoundException(id);  // Lève une exception si la ressource n'est pas trouvée
                }

                ressources.Remove(ressourceToRemove);  // Supprime la ressource
                JsonFileHandler.WriteToFile(FileName, ressources);  // Sauvegarde dans le fichier
            }
            catch (RessourceNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new RessourceSaveException();
            }
        }

        // Récupère toutes les ressources
        public List<Ressource> GetAll()
        {
            try
            {
                return JsonFileHandler.ReadFromFile<Ressource>(FileName);  // Récupère la liste des ressources à partir du fichier
            }
            catch (Exception)
            {
                throw new RessourceSaveException();
            }
        }

        // Récupère une ressource par son identifiant
        public Ressource GetById(Guid id)
        {
            try
            {
                var ressources = JsonFileHandler.ReadFromFile<Ressource>(FileName);
                var ressource = ressources.FirstOrDefault(r => r.Id == id);

                if (ressource == null)
                {
                    throw new RessourceNotFoundException(id);  // Lève une exception si la ressource n'est pas trouvée
                }

                return ressource;
            }
            catch (RessourceNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new RessourceSaveException();
            }
        }

        // Met à jour une ressource
        public void Update(Ressource ressource)
        {
            try
            {
                var ressources = JsonFileHandler.ReadFromFile<Ressource>(FileName);
                var index = ressources.FindIndex(r => r.Id == ressource.Id);

                if (index == -1)
                {
                    throw new RessourceNotFoundException(ressource.Id);  // Lève une exception si la ressource n'est pas trouvée
                }

                ressources[index] = ressource;  // Met à jour la ressource
                JsonFileHandler.WriteToFile(FileName, ressources);  // Sauvegarde dans le fichier
            }
            catch (RessourceNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new RessourceSaveException();
            }
        }
    }
}
