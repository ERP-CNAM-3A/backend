using System.Text.Json.Serialization;

namespace Domain.Entities.Ressources
{
    public sealed class Ressource
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int GroupId { get; private set; }
        public List<AvailabilityPeriod> AvailabilityPeriods { get; private set; } = new List<AvailabilityPeriod>();

        public Ressource(int id, string name, string type, int groupId)
        {
            Id = id;
            Name = name;
            Type = type;
            GroupId = groupId;
        }

        [JsonConstructor]
        public Ressource(int id, List<AvailabilityPeriod> availabilityPeriods)
        {
            Id = id;
            AvailabilityPeriods = availabilityPeriods;
        }

        public Ressource(int id, string name, string type, int groupId, List<AvailabilityPeriod> availabilityPeriods)
        {
            Id = id;
            Name = name;
            Type = type;
            GroupId = groupId;
            AvailabilityPeriods = availabilityPeriods;
        }

        public int DaysWorking
        {
            get => AvailabilityPeriods.Sum(p => (p.EndDate - p.StartDate).Days);
        }
    }

    public class AvailabilityPeriod
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AvailabilityPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
