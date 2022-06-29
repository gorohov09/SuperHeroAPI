namespace SuperHeroAPI.Domain.DTO
{
    public class SuperHeroTeamDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset DateOfCreation { get; set; }
    }
}
