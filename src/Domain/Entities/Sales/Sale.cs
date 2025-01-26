using Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Domain.Entities.Sales
{
    public sealed class Sale
    {
        public Guid Id { get; private set; }
        public SaleType Type { get; private set; }
        public Guid ClientId { get; private set; }
        public string What { get; private set; }
        public decimal Quantity { get; private set; }
        public SaleStatus Status { get; private set; }
        public double Chance { get; private set; }

        [JsonConstructor]
        public Sale(Guid id, SaleType type, Guid clientId, string what, decimal quantity, SaleStatus status, double chance)
        {
            Id = id;
            Type = type;
            ClientId = clientId;
            What = what;
            Quantity = quantity;
            Status = status;
            Chance = chance;
        }

        public Sale(SaleType type, Guid clientId, string what, decimal quantity, SaleStatus status, double chance)
        {
            Id = Guid.NewGuid();
            Type = type;
            ClientId = clientId;
            What = what;
            Quantity = quantity;
            Status = status;
            Chance = chance;
        }

        public void Update(SaleType type, Guid clientId, string what, decimal quantity, SaleStatus status, double chance)
        {
            Type = type;
            ClientId = clientId;
            What = what;
            Quantity = quantity;
            Status = status;
            Chance = chance;
        }

    }
}
