using System.Text.Json.Serialization;

namespace Domain.Entities.Ressources
{
    public sealed class Ressource
    {
        public Guid Id { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public int DaysWorking
        {

            get => (To - From).Days;
        }

        [JsonConstructor]
        public Ressource(Guid id, DateTime from, DateTime to)
        {
            Id = id;
            From = from;
            To = to;
        }

        public Ressource(DateTime from, DateTime to)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
        }

        public void Update(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
