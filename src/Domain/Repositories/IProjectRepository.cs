using Domain.Entities.Projects;

namespace Domain.Repositories
{
    public interface IProjectRepository
    {
        public void Add(Project project);
        public void Update(Project project);
        public void DeleteById(Guid id);
        public Project GetById(Guid id);
        public List<Project> GetAll();
    }
}
