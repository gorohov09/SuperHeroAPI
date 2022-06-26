using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        #region Сущности хранятся в памяти приложения
        //private static List<SuperHero> heroes = new List<SuperHero>
        //{
        //    new SuperHero { Id = 1,
        //            Name = "Spider Man",
        //            FirstName = "Peter",
        //            LastName = "Parker",
        //            Place = "New York City"
        //    },
        //    new SuperHero { Id = 2,
        //            Name = "Iron Man",
        //            FirstName = "Toni",
        //            LastName = "Stark",
        //            Place = "New York City"
        //    },
        //};
        #endregion

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(await _dataContext.SuperHeroes.ToListAsync());

        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> GetById(int Id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(Id);
            return hero is null ? NotFound() : Ok(hero);
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            await _dataContext.SuperHeroes.AddAsync(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHero hero)
        {
            var db_hero = await _dataContext.SuperHeroes.FindAsync(hero.Id);

            if (db_hero is null)
                return NotFound();

            db_hero.Name = hero.Name;
            db_hero.FirstName = hero.FirstName;
            db_hero.LastName = hero.LastName;
            db_hero.Place = hero.Place;

            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { db_hero.Id }, hero);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHero(int Id)
        {
            var db_hero = await _dataContext.SuperHeroes.FindAsync(Id);
            if (db_hero is null)
                return NotFound();

            _dataContext.SuperHeroes.Remove(db_hero);
            await _dataContext.SaveChangesAsync();

            return Ok(true);
        }
    }
}
