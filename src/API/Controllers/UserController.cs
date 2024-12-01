using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Ressources.Queries;
using Domain.Entities;
using API.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllRessources")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetRessources_Query();
            try
            {
                List<Ressource> ressources = await _mediator.Send(query);
                return Ok(ressources.Select(ressource => new RessourceLightDTO(ressource.Id, ressource.Name)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetRessourceById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetRessourceById_Query(id);
            try
            {
                Ressource ressource = await _mediator.Send(query);
                return Ok(new RessourceLightDTO(ressource.Id, ressource.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
