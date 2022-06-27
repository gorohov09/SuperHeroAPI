using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.Services.Interfaces
{
    public interface ISuperHeroRepository
    {
        Task<IEnumerable<SuperHero>> GetAll();

        Task<SuperHero?> GetById(int id);

        Task Add(SuperHero hero);

        Task<bool> Update(SuperHero hero);

        Task<bool> Delete(int id);
    }
}
