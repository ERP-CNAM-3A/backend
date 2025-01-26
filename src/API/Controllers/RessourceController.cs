using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Ressources.Commands;
using Application.UseCases.Ressources.Queries;
using Domain.Entities.Ressources;
using API.DTO.RessourceDTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RessourceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RessourceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET

        /// <summary>
        /// Retrieves all ressources.
        /// </summary>
        /// <returns>A list of all ressources.</returns>
        [HttpGet("GetAllRessources")]
        public async Task<IActionResult> GetAllRessources()
        {
            try
            {
                var query = new GetAllRessources_Query();
                List<Ressource> ressources = await _mediator.Send(query);
                return Ok(ressources.Select(ressource => new RessourceDTO(ressource)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a ressource by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the ressource.</param>
        /// <returns>The details of the ressource with the specified ID.</returns>
        [HttpGet("GetRessourceById/{id}")]
        public async Task<IActionResult> GetRessourceById(Guid id)
        {
            var query = new GetRessourceById_Query(id);
            try
            {
                Ressource ressource = await _mediator.Send(query);
                return Ok(new RessourceDTO(ressource));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Creates a new ressource.
        /// </summary>
        /// <param name="input">The ressource data transfer object containing information for creating the ressource.</param>
        /// <returns>The details of the newly created ressource.</returns>
        [HttpPost("CreateRessource")]
        public async Task<IActionResult> CreateRessource(RessourceDTO input)
        {
            var command = new CreateRessource_Command(
                input.Name, input.Type, input.DailyRate);
            try
            {
                Ressource createdRessource = await _mediator.Send(command);
                return Ok(new RessourceDTO(createdRessource));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region PUT

        /// <summary>
        /// Updates the details of an existing ressource by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the ressource.</param>
        /// <param name="input">The ressource data transfer object containing the updated details of the ressource.</param>
        /// <returns>The details of the updated ressource.</returns>
        [HttpPut("UpdateRessource/{id}")]
        public async Task<IActionResult> UpdateRessource(Guid id, UpdateRessourceDTO input)
        {
            var command = new UpdateRessource_Command(id, input.Name, input.Type, input.DailyRate);
            try
            {
                Ressource ressourceUpdated = await _mediator.Send(command);
                return Ok(new RessourceDTO(ressourceUpdated));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a ressource by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the ressource to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("DeleteRessource/{id}")]
        public async Task<IActionResult> DeleteRessource(Guid id)
        {
            var command = new DeleteRessource_Command(id);
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
