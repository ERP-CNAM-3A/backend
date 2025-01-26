using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Sales.Commands;
using Application.UseCases.Sales.Queries;
using Domain.Entities.Sales;
using API.DTO.SaleDTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>A list of all sales.</returns>
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var query = new GetAllSales_Query();
                List<Sale> sales = await _mediator.Send(query);
                return Ok(sales.Select(sale => new SaleDTO(sale)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <returns>The details of the sale with the specified ID.</returns>
        [HttpGet("GetSaleById/{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var query = new GetSaleById_Query(id);
            try
            {
                Sale sale = await _mediator.Send(query);
                return Ok(new SaleDTO(sale));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="input">The sale data transfer object containing information for creating the sale.</param>
        /// <returns>The details of the newly created sale.</returns>
        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale(SaleDTO input)
        {
            var command = new CreateSale_Command(
                input.Type,
                input.ClientId,
                input.What,
                input.Quantity,
                input.Status,
                input.Chance
            );
            try
            {
                Sale createdSale = await _mediator.Send(command);
                return Ok(new SaleDTO(createdSale));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region PUT

        /// <summary>
        /// Updates the details of an existing sale by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <param name="input">The sale data transfer object containing the updated details of the sale.</param>
        /// <returns>The details of the updated sale.</returns>
        [HttpPut("UpdateSale/{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, UpdateSaleDTO input)
        {
            var command = new UpdateSale_Command(
                id,
                input.Type,
                input.ClientId,
                input.What,
                input.Quantity,
                input.Status,
                input.Chance
            );
            try
            {
                Sale updatedSale = await _mediator.Send(command);
                return Ok(new SaleDTO(updatedSale));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a sale by its unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the sale to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("DeleteSale/{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var command = new DeleteSale_Command(id);
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
