using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Controllers
{
    [Route("api/ability")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IAbilityRepository _AbilityService;
        private readonly IMapper _Mapper;

        public AbilityController(IAbilityRepository AbilityService, IMapper Mapper)
        {
            _AbilityService = AbilityService;
            _Mapper = Mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var abilities = await _AbilityService.GetAbilites();
            var abilities_dto = _Mapper.Map<IEnumerable<AbilityDTO>>(abilities);
            return Ok(abilities_dto);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var ability = await _AbilityService.GetAbilityById(Id);
            if (ability is null)
                return NotFound();
            var ability_dto = _Mapper.Map<AbilityDTO>(ability);
            return Ok(ability_dto);
        }
    }
}
