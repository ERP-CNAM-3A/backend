using API.DTO.ProjectDTOs;
using Application.UseCases.Projects.Commands;
using Application.UseCases.Projects.Queries;
using Domain.Entities.Projects;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SyncProjectsService _syncProjectsService;

        public ProjectController(IMediator mediator, SyncProjectsService syncProjectsService)
        {
            _mediator = mediator;
            _syncProjectsService = syncProjectsService;
        }

        #region GET

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        /// <returns>A list of all projects.</returns>
        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var query = new GetAllProjects_Query();
                List<Project> projects = await _mediator.Send(query);
                return Ok(projects.Select(project => new ProjectDTO(project)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a project by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The details of the project with the specified ID.</returns>
        [HttpGet("GetProjectById/{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var query = new GetProjectById_Query(id);
            try
            {
                Project project = await _mediator.Send(query);
                return Ok(new ProjectDTO(project));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Launches a simulation to verify if projects can be delivered on time.
        /// </summary>
        /// <returns>A list of simulation results, including project delivery feasibility.</returns>
        [HttpGet("SimulateProjectsDelivery")]
        public async Task<IActionResult> SimulateProjectsDelivery()
        {
            var query = new GetProjectsSimulations_Query();
            try
            {
                var results = await _mediator.Send(query);
                return Ok(results);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Launches a simulation to verify if a specific project can be delivered on time based on its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the project to simulate.</param>
        /// <returns>A simulation result for the project, including its delivery feasibility.</returns>
        [HttpGet("SimulateProjectDeliveryById/{id}")]
        public async Task<IActionResult> SimulateProjectDeliveryById(Guid id)
        {
            var query = new GetProjectSimulationById_Query(id);
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region POST

        /// <summary>
        /// Syncs projects for all external sales.
        /// This operation will synchronize the external sales data with the projects.
        /// </summary>
        /// <returns>A response indicating the success or failure of the synchronization process.</returns>
        [HttpPost("SyncProjects")]
        public async Task<IActionResult> SyncProjects()
        {
            try
            {
                await _syncProjectsService.SyncProjectsAsync();
                return Ok("Projects successfully synced for all external sales.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error syncing projects: {e.Message}");
            }
        }

        #endregion

        #region PUT

        /// <summary>
        /// Updates the details of an existing project by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="input">The project data transfer object containing the updated details of the project.</param>
        /// <returns>The details of the updated project.</returns>
        [HttpPut("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectDTO input)
        {
            var command = new UpdateProject_Command(
                id,
                input.WorkDaysNeeded,
                input.Ressources
            );
            try
            {
                Project projectUpdated = await _mediator.Send(command);
                return Ok(new ProjectDTO(projectUpdated));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a project by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the project to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var command = new DeleteProject_Command(id);
            try
            {
                await _mediator.Send(command);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
