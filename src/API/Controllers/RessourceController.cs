using API.DTO.RessourceDTOs;
using Application.UseCases.Ressources.Queries;
using Domain.Entities.Ressources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
                var result = ressources.Select(r => new LightRessourceDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type
                }).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves available ressources between the specified dates.
        /// </summary>
        /// <param name="dto">The DTO containing the start and end dates.</param>
        /// <returns>A list of available ressources between the specified dates.</returns>
        [HttpPost("GetAvailableRessourcesBetween")]
        public async Task<IActionResult> GetAvailableRessourcesBetween([FromBody] AvailabilityPeriodDTO dto)
        {
            var query = new GetAvailableRessourcesBetween_Query(dto.StartDate, dto.EndDate);
            try
            {
                List<Ressource> ressources = await _mediator.Send(query);
                var result = ressources.Select(r => new RessourceWithAvailabilityDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = r.Type,
                    AvailabilityPeriods = r.AvailabilityPeriods.Select(p => new AvailabilityPeriodDTO
                    {
                        StartDate = p.StartDate,
                        EndDate = p.EndDate
                    }).ToList()
                }).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
