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
        /// Method responsible for syncing projects by checking external sales and creating missing projects.
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

                // Loop through each external sale and check if it already has a corresponding project
                foreach (var sale in externalSales)
                {
                    // Check if a project already exists for this external sale (based on the Sale's ID)
                    var existingProject = existingProjects.FirstOrDefault(p => p.Sale.Id == sale.Id);

                    // If no project exists for this sale, proceed to create a new project
                    if (existingProject == null)
                    {
                        // Generate a random number of days for the project (between 1 and 30)
                        var random = new Random();
                        double randomDays = random.Next(1, 30);

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
            catch (Exception e)
            {
                // Log or handle the exception accordingly (rethrowing for now)
                throw new Exception($"Error syncing projects: {e.Message}");
            }
        }
    }
}
