using API.DTO.SaleDTOs;
using Domain.Entities.Sales;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ExternalSaleService _externalSaleService;

        public SaleController(ExternalSaleService externalSaleService)
        {
            _externalSaleService = externalSaleService;
        }

        #region GET

        /// <summary>
        /// Retrieves external sales and returns them as DTOs.
        /// </summary>
        /// <returns>A list of external sales as DTOs.</returns>
        [HttpGet("GetExternalSales")]
        public async Task<IActionResult> GetExternalSales()
        {
            try
            {
                List<Sale> externalSales = await _externalSaleService.GetExternalSalesAsync();

                var salesDto = externalSales.Select(s => new SaleDTO
                {
                    Id = s.Id,
                    Type = s.Type,
                    Client = s.Client,
                    Product = s.Product,
                    Amount = s.Amount,
                    Date = s.Date,
                    Status = s.Status,
                    Probability = s.Probability
                });

                return Ok(salesDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion
    }
}
