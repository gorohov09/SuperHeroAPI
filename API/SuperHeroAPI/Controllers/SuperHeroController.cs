using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _SuperHeroService;
        private readonly IMapper _Mapper;

        public SuperHeroController(ISuperHeroRepository SuperHeroService, IMapper Mapper)
        {
            _SuperHeroService = SuperHeroService;
            _Mapper = Mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var heroes = await _SuperHeroService.GetAll();
            var heroes_dto = _Mapper.Map<List<SuperHeroDTO>>(heroes);
            return Ok(heroes_dto);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var hero = await _SuperHeroService.GetById(Id);
            if (hero is null)
                return NotFound();

            var hero_dto = _Mapper.Map<SuperHeroDTO>(hero);
            return Ok(hero_dto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddHero(SuperHeroDTO hero_dto)
        {
            if (hero_dto is null)
                return NotFound();

            var hero = _Mapper.Map<SuperHero>(hero_dto);
            await _SuperHeroService.Add(hero);
            var heroes = await _SuperHeroService.GetAll();
            var heroes_dto = _Mapper.Map<List<SuperHeroDTO>>(heroes);
            return Ok(heroes_dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHeroDTO hero_dto)
        {
            if (hero_dto is null)
                return NotFound();

            var hero = _Mapper.Map<SuperHero>(hero_dto);
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
