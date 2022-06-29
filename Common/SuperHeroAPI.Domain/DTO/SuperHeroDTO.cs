namespace SuperHeroAPI.Domain.DTO
{
    public class SuperHeroDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? RealName { get; set; }

        public string? RealLastName { get; set; }

        public string Type { get; set; } = string.Empty;

        public int Height { get; set; }

        public int Weight { get; set; }

        public string? Place { get; set; }

        public ICollection<AbilityDTO> Abilities { get; set; } = new List<AbilityDTO>();

        public SuperHeroTeamDTO? Team { get; set; }
    }
}
