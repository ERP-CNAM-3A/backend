using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Projects.Commands;
using Application.UseCases.Projects.Queries;
using Domain.Entities.Projects;
using API.DTO.ProjectDTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
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

        #endregion

        #region POST

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="input">The project data transfer object containing information for creating the project.</param>
        /// <returns>The details of the newly created project.</returns>
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectDTO input)
        {
            var command = new CreateProject_Command(
                input.Name,
                input.ProjectType,
                input.SaleType,
                input.DueDate,
                input.DaysRequired,
                input.Status
            );
            try
            {
                Project createdProject = await _mediator.Send(command);
                return Ok(new ProjectDTO(createdProject));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                input.Name,
                input.ProjectType,
                input.SaleType,
                input.DueDate,
                input.DaysRequired,
                input.Status
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
