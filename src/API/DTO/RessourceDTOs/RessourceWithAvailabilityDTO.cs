namespace API.DTO.RessourceDTOs
{
    public sealed class RessourceWithAvailabilityDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public List<AvailabilityPeriodDTO>? AvailabilityPeriods { get; set; }
    }
}
