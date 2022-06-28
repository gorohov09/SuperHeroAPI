namespace SuperHeroAPI.Domain.Entities
{
    public class SuperHero
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? RealName { get; set; }

        public string? RealLastName { get; set; }

        public string Type { get; set; } = string.Empty;

        public int Height { get; set; }

        public int Weight { get; set; }

        public string? Place { get; set; }

        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();

        public int SuperHeroTeamId { get; set; }
        public SuperHeroTeam Team { get; set; }
    }
}
