using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero { Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
            },
            new SuperHero { Id = 2,
                    Name = "Iron Man",
                    FirstName = "Toni",
                    LastName = "Stark",
                    Place = "New York City"
            },
        };

        [HttpGet("all")]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(heroes);

        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> GetById(int Id)
        {
            var hero = heroes.Find(hero => hero.Id == Id);
            return hero is null ? NotFound() : Ok(hero);
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpPut]
        public async Task<IActionResult> PutHero(SuperHero hero)
        {
            var hero_old = heroes.Find(h => h.Id == hero.Id);

            if (hero_old is null)
                return NotFound();

            hero_old.Name = hero.Name;
            hero_old.FirstName = hero.FirstName;
            hero_old.LastName = hero.LastName;
            hero_old.Place = hero.Place;

            return CreatedAtAction(nameof(GetById), new { hero_old.Id }, hero);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHero(int Id)
        {
            var hero = heroes.Find(h => h.Id == Id);
            if (hero is null)
                return NotFound();

            var success = heroes.Remove(hero);
            return Ok(success);
        }
    }
}
