using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<SuperHeroTeam> SuperHeroTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperHero>().ToTable("SuperHeroes");
            modelBuilder.Entity<Ability>().ToTable("Abilities");
            modelBuilder.Entity<SuperHeroTeam>().ToTable("SuperHeroTeams");

            modelBuilder.Entity<SuperHero>().Property(s => s.Name).IsRequired();
            modelBuilder.Entity<SuperHeroTeam>().Property(t => t.Name).IsRequired();

            modelBuilder.Entity<SuperHero>()
                .HasOne(s => s.Team)
                .WithMany(t => t.Heroes)
                .HasForeignKey(s => s.SuperHeroTeamId);

            modelBuilder.Entity<Ability>()
                .HasMany(a => a.SuperHeroes)
                .WithMany(s => s.Abilities)
                .UsingEntity(j => j.ToTable("Heroes_Abilities"));
        }
    }
}
