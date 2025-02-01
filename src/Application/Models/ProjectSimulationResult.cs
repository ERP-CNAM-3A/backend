namespace Application.Models
{
    public sealed class ProjectSimulationResult
    {
        public Guid ProjectId { get; set; }
        public int RessourcesAssigned { get; set; }
        public double WorkDays { get; set; }
        public double AvailableWorkDays { get; set; }
        public double RemainingNeededWorkDays { get; set; }
        public bool CanBeDeliveredOnTime { get; set; }
    }
}
