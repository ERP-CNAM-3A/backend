using API.DTO;
using Application.UseCases.Projects.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProjectDTO input)
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
                Project project = await _mediator.Send(command);
                return Ok(project);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
