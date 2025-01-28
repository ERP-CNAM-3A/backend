using System.Text.Json.Serialization;

namespace Domain.Entities.Ressources
{
    public sealed class Ressource
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int DailyRate { get; private set; }


        [JsonConstructor]
        public Ressource(Guid id, string name, int dailyRate)
        {
            Id = id;
            Name = name;
            DailyRate = dailyRate;
        }

        public Ressource(string name, int dailyRate)
        {
            Name = name;
            DailyRate = dailyRate;
        }

        public void Update(string name, int dailyRate)
        {
            Name = name;
            DailyRate = dailyRate;
        }
    }
}
