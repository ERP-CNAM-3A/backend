using Domain.Entities.Sales;

namespace API.DTO.SaleDTOs
{
    public sealed class UpdateSaleDTO
    {
        public string Type { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Probability { get; set; }

        public UpdateSaleDTO() { }

        public UpdateSaleDTO(Sale sale)
        {
            Type = sale.Type;
            Client = sale.Client;
            Product = sale.Product;
            Amount = sale.Amount;
            Status = sale.Status;
            Probability = sale.Probability;
        }
    }
}
