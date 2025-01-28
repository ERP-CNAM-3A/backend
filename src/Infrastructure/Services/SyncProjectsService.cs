using Application.UseCases.Projects.Commands;
using Domain.Entities.Projects;
using Domain.Entities.Ressources;
using Domain.Entities.Sales;
using Domain.Repositories;
using MediatR;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service responsible for syncing projects by fetching external sales and creating projects accordingly.
    /// </summary>
    public class SyncProjectsService
    {
        private readonly ExternalSaleService _externalSaleService;
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor to inject dependencies for external sales service, project repository, and mediator.
        /// </summary>
        public SyncProjectsService(
            ExternalSaleService externalSaleService,
            IProjectRepository projectRepository,
            IMediator mediator)
        {
            _externalSaleService = externalSaleService;
            _projectRepository = projectRepository;
            _mediator = mediator;
        }


        /// <summary>
        /// Method responsible for syncing projects by checking external sales, creating missing projects, 
        /// and deleting projects that no longer have a corresponding sale.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SyncProjectsAsync()
        {
            try
            {
                // Fetch external sales from the external API
                List<Sale> externalSales = await _externalSaleService.GetExternalSalesAsync();

                // Fetch all existing projects from the database to perform the delta check
                var existingProjects = _projectRepository.GetAll();

                // Create new projects for external sales that do not have corresponding projects
                await CreateMissingProjects(externalSales, existingProjects);

                // Delete projects that no longer have a corresponding external sale
                await DeleteOrphanedProjects(externalSales, existingProjects);
            }
            catch (Exception e)
            {
                // Log or handle the exception accordingly (rethrowing for now)
                throw new Exception($"Error syncing projects: {e.Message}");
            }
        }

        /// <summary>
        /// Creates missing projects based on external sales that do not have corresponding projects.
        /// </summary>
        /// <param name="externalSales">The list of external sales.</param>
        /// <param name="existingProjects">The list of existing projects in the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task CreateMissingProjects(List<Sale> externalSales, List<Project> existingProjects)
        {
            foreach (var sale in externalSales)
            {
                // Check if a project already exists for this external sale (based on the Sale's ID)
                var existingProject = existingProjects.FirstOrDefault(p => p.Sale.Id == sale.Id);

                // If no project exists for this sale, proceed to create a new project
                if (existingProject == null)
                {
                    // Generate a random number of days for the project (between 1 and 30)
                    var randomDays = new Random().Next(1, 30);

                    // Create a new project with the external sale, random days, and empty resources list
                    var project = new Project
                    {
                        Sale = sale,
                        Days = randomDays,
                        Ressources = new List<Ressource>()
                    };

                    // Create the command to create the project in the system
                    var command = new CreateProject_Command(
                        project.Sale,
                        project.Days,
                        project.Ressources
                    );

                    // Send the command to the mediator to create the project
                    await _mediator.Send(command);
                }
            }
        }

        /// <summary>
        /// Deletes orphaned projects that no longer have a corresponding sale in the external sales list.
        /// </summary>
        /// <param name="externalSales">The list of external sales.</param>
        /// <param name="existingProjects">The list of existing projects in the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task DeleteOrphanedProjects(List<Sale> externalSales, List<Project> existingProjects)
        {
            foreach (var existingProject in existingProjects)
            {
                // Check if the sale related to the project still exists in the external sales list
                var correspondingSale = externalSales.FirstOrDefault(s => s.Id == existingProject.Sale.Id);

                // If the sale no longer exists, delete the project
                if (correspondingSale == null)
                {
                    // Delete the orphaned project
                    _projectRepository.DeleteById(existingProject.Id);
                }
            }
        }

    }
}
