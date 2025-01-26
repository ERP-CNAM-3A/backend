using Domain.Entities.Sales;
using Domain.Enums;

namespace API.DTO.SaleDTOs
{
    public sealed class SaleDTO
    {
        public Guid Id { get; set; }
        public SaleType Type { get; set; }
        public Guid ClientId { get; set; }
        public string What { get; set; }
        public decimal Quantity { get; set; }
        public SaleStatus Status { get; set; }
        public double Chance { get; set; }
        
        public SaleDTO() { }

        public SaleDTO(Sale sale)
        {
            Id = sale.Id;
            Type = sale.Type;
            ClientId = sale.ClientId;
            What = sale.What;
            Quantity = sale.Quantity;
            Status = sale.Status;
            Chance = sale.Chance;
        }
    }
}
