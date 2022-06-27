using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
