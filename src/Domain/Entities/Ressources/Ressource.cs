using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities.Ressources
{
    public sealed class Ressource
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public RessourceType Type { get; private set; }
        public int DailyRate { get; private set; }


        [JsonConstructor]
        public Ressource(Guid id, string name, RessourceType type, int dailyRate)
        {
            Id = id;
            Name = name;
            Type = type;
            DailyRate = dailyRate;
        }

        public Ressource(string name, RessourceType type, int dailyRate)
        {
            Name = name;
            Type = type;
            DailyRate = dailyRate;
        }

        public void Update(string name, RessourceType type, int dailyRate)
        {
            Name = name;
            Type = type;
            DailyRate = dailyRate;
        }
    }
}
