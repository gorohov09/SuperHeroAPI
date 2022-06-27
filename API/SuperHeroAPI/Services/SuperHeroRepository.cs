using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Interfaces;

namespace SuperHeroAPI.Services
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
            var db_hero = await _Context.SuperHeroes.FirstOrDefaultAsync(hero => hero.Id == id)
                .ConfigureAwait(false);
            if (db_hero is null)
                _Logger.LogWarning("Супергероя под Id:{0} не существует", id);
            else
                _Logger.LogInformation("Информация о супергерое под Id:{0} была получена", db_hero.Id);
            return db_hero;
        }

        public async Task<IEnumerable<SuperHero>> GetAll()
        {
            var db_heroes = await _Context.SuperHeroes.ToListAsync();
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
            db_hero.LastName = hero.LastName;
            db_hero.FirstName = hero.FirstName;
            db_hero.Place = hero.Place;
            await _Context.SaveChangesAsync();

            _Logger.LogInformation("Информация о супергерое под Id:{0} была изменена", db_hero.Id);
            return true;
        }
    }
}
