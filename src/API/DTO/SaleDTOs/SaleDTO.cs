using Domain.Entities.Sales;

namespace API.DTO.SaleDTOs
{
    public sealed class SaleDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Probability { get; set; }

        public SaleDTO() { }

        public SaleDTO(Sale sale)
        {
            Id = sale.Id;
            Type = sale.Type;
            Client = sale.Client;
            Product = sale.Product;
            Amount = sale.Amount;
            Date = sale.Date;
            Status = sale.Status;
            Probability = sale.Probability;
        }
    }
}
