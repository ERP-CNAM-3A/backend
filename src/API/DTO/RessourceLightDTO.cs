namespace API.DTO
{
    public sealed class RessourceLightDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RessourceLightDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
