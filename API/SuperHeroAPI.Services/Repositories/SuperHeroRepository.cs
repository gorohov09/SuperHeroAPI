using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperHeroAPI.DAL.Context;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Services.Repositories
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<SuperHeroRepository> _Logger;

        public SuperHeroRepository(DataContext Context, ILogger<SuperHeroRepository> Logger)
        {
            _Context = Context;
            _Logger = Logger;
        }

        public async Task Add(SuperHero hero)
        {
            if (hero is null)
            {
                _Logger.LogWarning("Пытаемся добавить супергероя, у которого значение null");
                throw new ArgumentNullException(nameof(hero));
            }
            await _Context.AddAsync(hero);
            _Logger.LogInformation("Супергерой был добавлен и получил Id:{0}", hero.Id);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var db_hero = await _Context.SuperHeroes.FirstOrDefaultAsync(h => h.Id == id)
                .ConfigureAwait(false);
            if (db_hero is null)
            {
                _Logger.LogWarning("Пытаемся удалить несуществующего супергероя");
                return false;
            }
            _Context.SuperHeroes.Remove(db_hero);
            await _Context.SaveChangesAsync();
            _Logger.LogInformation("Супергерой под Id:{0} был удален", db_hero.Id);
            return true;
        }

        public async Task<SuperHero?> GetById(int id)
        {
            var db_hero = await _Context.SuperHeroes
                .Include(t => t.Team)
                .Include(s => s.Abilities)
                .FirstOrDefaultAsync(hero => hero.Id == id)
                .ConfigureAwait(false);

            if (db_hero is null)
                _Logger.LogWarning("Супергероя под Id:{0} не существует", id);
            else
                _Logger.LogInformation("Информация о супергерое под Id:{0} была получена", db_hero.Id);
            return db_hero;
        }

        public async Task<IEnumerable<SuperHero>> GetAll()
        {
            var db_heroes = await _Context.SuperHeroes
                .Include(t => t.Team)
                .Include(s => s.Abilities)
                .ToListAsync()
                .ConfigureAwait(false);

            _Logger.LogInformation("Супергерои в колличестве:{0} были получены", db_heroes.Count);
            return db_heroes;
        }

        public async Task<bool> Update(SuperHero hero)
        {
            var db_hero = await _Context.SuperHeroes.FirstOrDefaultAsync(h => h.Id == hero.Id)
                .ConfigureAwait(false);
            if (db_hero is null)
            {
                _Logger.LogWarning("Пытаемся обновить несуществующего супергероя");
                return false;
            }

            db_hero.Name = hero.Name;
            db_hero.RealLastName = hero.RealLastName;
            db_hero.RealName = hero.RealName;
            db_hero.Type = hero.Type;
            db_hero.Height = hero.Height;
            db_hero.Weight = hero.Weight;
            db_hero.Place = hero.Place;
            await _Context.SaveChangesAsync();

            _Logger.LogInformation("Информация о супергерое под Id:{0} была изменена", db_hero.Id);
            return true;
        }

        public async Task<bool> AddAbility(int heroId, AbilityDTO abilityDTO)
        {
            var db_hero = await _Context.SuperHeroes.FirstOrDefaultAsync(h => h.Id == heroId)
                .ConfigureAwait(false);
            if (db_hero is null)
            {
                _Logger.LogWarning("Пытаемся добавить cуперспособность несуществующему супергерою");
                return false;
            }

            if (abilityDTO is null || string.IsNullOrEmpty(abilityDTO.Description))
            {
                _Logger.LogWarning("Суперспобность не существует или описание суперспособности отсутствует");
                return false;
            }

            if (abilityDTO.Id == 0)
            {
                var db_ability = await _Context.Abilities.FirstOrDefaultAsync(a => a.Description == abilityDTO.Description);
                if (db_ability is null)
                {
                    _Logger.LogWarning("Суперспособность: {0} не существует. Создаем свою", abilityDTO.Description);
                    db_ability = new Ability { Description = abilityDTO.Description };
                }

                db_hero.Abilities.Add(db_ability);
            }
            else
            {
                var db_ability = await _Context.Abilities.FirstOrDefaultAsync(a => a.Id == abilityDTO.Id);
                if (db_ability is null)
                {
                    _Logger.LogWarning("Суперспособность с Id: {0} не существует.", abilityDTO.Id);
                    return false;
                }
            }
            await _Context.SaveChangesAsync();
            return true;
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
    }
}
