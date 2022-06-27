using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Interfaces;

namespace SuperHeroAPI.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _SuperHeroService;

        public SuperHeroController(ISuperHeroRepository SuperHeroService)
        {
            _SuperHeroService = SuperHeroService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<SuperHero>>> Get() => Ok(await _SuperHeroService.GetAll());

        [HttpGet("{Id}")]
        public async Task<ActionResult<SuperHero>> GetById(int Id)
        {
            var hero = await _SuperHeroService.GetById(Id);
            return hero is null ? NotFound() : Ok(hero);
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            await _SuperHeroService.Add(hero);
            return Ok(await _SuperHeroService.GetAll());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHero hero)
        {
            var result = await _SuperHeroService.Update(hero);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteHero(int Id)
        {
            var result = await _SuperHeroService.Delete(Id);
            return Ok(result);
        }
    }
}
