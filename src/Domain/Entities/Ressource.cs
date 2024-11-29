using Domain.Enums;

namespace Domain.Entities
{
    public sealed class Ressource
    {
        public Guid Id { get; }
        public string Name { get; }
        public RessourceType Type { get; }
        public int DailyRate { get; }


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
    }
}
