using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.Services.Interfaces
{
    public interface IAbilityRepository
    {
        Task<IEnumerable<Ability>> GetAbilites();

        Task<Ability> GetAbilityById(int id);

    }
}
