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
        private readonly ILogger<RessourceController> _logger;

        public RessourceController(IMediator mediator, ILogger<RessourceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        #region GET

        /// <summary>
        /// Retrieves all ressources.
        /// </summary>
        /// <returns>A list of all ressources.</returns>
        [HttpGet("GetAllRessources")]
        public async Task<IActionResult> GetAllRessources()
        {
            _logger.LogInformation("GetAllRessources called");
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
                _logger.LogInformation("GetAllRessources succeeded");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllRessources failed");
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
            _logger.LogInformation("GetAvailableRessourcesBetween called with StartDate: {StartDate}, EndDate: {EndDate}", dto.StartDate, dto.EndDate);
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
                _logger.LogInformation("GetAvailableRessourcesBetween succeeded");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAvailableRessourcesBetween failed");
                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
