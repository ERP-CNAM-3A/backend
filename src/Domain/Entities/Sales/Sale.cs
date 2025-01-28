using System.Text.Json.Serialization;

namespace Domain.Entities.Sales
{
    public sealed class Sale
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Client { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Probability { get; set; }

        public Sale() { }

        [JsonConstructor]
        public Sale(Guid id, string type, string client, string product, decimal amount, string date, string status, string probability)
        {
            Id = id;
            Type = type;
            Client = client;
            Product = product;
            Amount = amount;
            Date = date;
            Status = status;
            Probability = probability;
        }

        public Sale(string type, string client, string product, decimal amount, string date, string status, string probability)
        {
            Id = Guid.NewGuid();
            Type = type;
            Client = client;
            Product = product;
            Amount = amount;
            Date = date;
            Status = status;
            Probability = probability;
        }

        public void Update(string type, string client, string product, decimal amount, string date, string status, string probability)
        {
            Type = type;
            Client = client;
            Product = product;
            Amount = amount;
            Date = date;
            Status = status;
            Probability = probability;
        }

    }
}
