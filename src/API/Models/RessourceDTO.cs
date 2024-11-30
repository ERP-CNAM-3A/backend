using Domain.Enums;

namespace API.Models
{
    public class RessourceDto
    {
        public string Name { get; set; }
        public RessourceType Type { get; set; }
        public int DailyRate { get; set; }
    }
}
