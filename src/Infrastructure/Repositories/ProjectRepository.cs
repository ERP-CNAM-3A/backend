using Domain.Entities.Projects;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    internal sealed class ProjectRepository : IProjectRepository
    {
        private const string FileName = "projects.json"; // Fichier de données pour les projets
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ILogger<ProjectRepository> logger)
        {
            _logger = logger;
        }


        // Ajouter un projet
        public void Add(Project project)
        {
            try
            {
                var projects = JsonFileHandler.ReadFromFile<Project>(FileName);

                if (projects.Any(p => p.Id == project.Id))
                {
                    throw new ProjectAlreadyExistsException(project.Id);
                }

                projects.Add(project);
                JsonFileHandler.WriteToFile(FileName, projects);
            }
            catch (Exception)
            {
                throw new ProjectSaveException();
            }
        }

        // Supprimer un projet par ID
        public void DeleteById(Guid id)
        {
            try
            {
                var projects = JsonFileHandler.ReadFromFile<Project>(FileName);
                var projectToRemove = projects.FirstOrDefault(p => p.Id == id);

                if (projectToRemove == null)
                {
                    throw new ProjectNotFoundException(id);
                }

                projects.Remove(projectToRemove);
                JsonFileHandler.WriteToFile(FileName, projects);
            }
            catch (ProjectNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ProjectSaveException();
            }
        }

        // Récupérer tous les projets
        public List<Project> GetAll()
        {
            try
            {
                return JsonFileHandler.ReadFromFile<Project>(FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while GetAll");
                throw new ProjectSaveException();
            }
        }

        // Récupérer un projet par ID
        public Project GetById(Guid id)
        {
            try
            {
                var projects = JsonFileHandler.ReadFromFile<Project>(FileName);
                var project = projects.FirstOrDefault(p => p.Id == id);

                if (project == null)
                {
                    throw new ProjectNotFoundException(id);
                }

                return project;
            }
            catch (ProjectNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ProjectSaveException();
            }
        }

        // Mettre à jour un projet
        public void Update(Project project)
        {
            try
            {
                var projects = JsonFileHandler.ReadFromFile<Project>(FileName);
                var index = projects.FindIndex(p => p.Id == project.Id);

                if (index == -1)
                {
                    throw new ProjectNotFoundException(project.Id);
                }

                projects[index] = project;
                JsonFileHandler.WriteToFile(FileName, projects);
            }
            catch (ProjectNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ProjectSaveException();
            }
        }
    }
}
