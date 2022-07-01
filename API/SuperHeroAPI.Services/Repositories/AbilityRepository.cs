using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperHeroAPI.DAL.Context;
using SuperHeroAPI.Domain.Entities;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Services.Repositories
{
    public class AbilityRepository : IAbilityRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<AbilityRepository> _Logger;

        public AbilityRepository(DataContext Context, ILogger<AbilityRepository> Logger)
        {
            _Context = Context;
            _Logger = Logger;
        }

        public async Task<IEnumerable<Ability>> GetAbilites()
        {
            var abilities = await _Context.Abilities.ToListAsync();
            return abilities;
        }

        public async Task<Ability> GetAbilityById(int id)
        {
            var ability = await _Context.Abilities
                .FirstOrDefaultAsync(a => a.Id == id);
            if (ability is null)
                _Logger.LogWarning("Суперспособности с Id:{0} не существует", id);
            return ability;
        }
    }
}
