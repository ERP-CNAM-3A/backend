using Domain.Enums;

namespace API.DTO.ProjectDTOs
{
    public sealed class CreateProjectDTO
    {
        public required string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime DueDate { get; set; }
        public int DaysRequired { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
