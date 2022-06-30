using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperHeroAPI.DAL.Context;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Services.Repositories
{
    public class SuperHeroTeamRepository : ISuperHeroTeamRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<SuperHeroTeamRepository> _Logger;

        public SuperHeroTeamRepository(DataContext Context, ILogger<SuperHeroTeamRepository> Logger)
        {
            _Context = Context;
            _Logger = Logger;
        }

        public async Task<bool> AddHeroInGroup(int heroId, AddHeroInGroupDTO group)
        {
            var db_hero = await _Context.SuperHeroes
                .Include(t => t.Team)
                .FirstOrDefaultAsync(hero => hero.Id == heroId)
                .ConfigureAwait(false);

            if (db_hero is null)
            {
                _Logger.LogWarning("Супергерой с Id:{0} не найден", heroId);
                return false;
            }
            if (db_hero.Team != null)
            {
                _Logger.LogWarning("У Супергероя с Id:{0} группа уже есть", heroId);
                return false;
            }

            var db_group = await _Context.SuperHeroTeams
                .FirstOrDefaultAsync(t => t.Name == group.Name);
            if (db_group is null)
            {
                _Logger.LogInformation("Группа: {0} не найдена. Начинаем процесс создания новой", group.Name);
                db_group = new SuperHeroTeam
                {
                    Name = group.Name,
                    DateOfCreation = (DateTimeOffset)group.DateOfCreation!,
                };
            }   
            else
                _Logger.LogInformation("Группа: {0} найдена", group.Name);
            
            db_hero.Team = db_group;
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHeroInGroup(int heroId)
        {
            var db_hero = await _Context.SuperHeroes
                .Include(t => t.Team)
                .FirstOrDefaultAsync(hero => hero.Id == heroId)
                .ConfigureAwait(false);

            if (db_hero is null)
            {
                _Logger.LogWarning("Супергерой с Id:{0} не найден", heroId);
                return false;
            }

            if (db_hero.Team is null)
            {
                _Logger.LogWarning("Супергерой с Id:{0} не состоит в группе", heroId);
                return false;
            }

            db_hero.Team = null;
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<SuperHeroTeam> GetById(int id)
        {
            var group = await _Context.SuperHeroTeams
                .FirstOrDefaultAsync(t => t.Id == id);

            return group;   
        }
    }
}
