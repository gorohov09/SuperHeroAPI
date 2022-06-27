namespace SuperHeroAPI.Interfaces
{
    public interface ISuperHeroRepository
    {
        IEnumerable<SuperHero> GetAll();

        SuperHero Get(int id);

        void Add(SuperHero hero);

        bool Update(SuperHero hero);

        bool Delete(int id);
    }
}
