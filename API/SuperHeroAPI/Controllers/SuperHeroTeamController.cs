using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Services.Interfaces;

namespace SuperHeroAPI.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class SuperHeroTeamController : ControllerBase
    {
        private readonly ISuperHeroTeamRepository _SuperHeroTeamService;
        private readonly IMapper _Mapper;

        public SuperHeroTeamController(ISuperHeroTeamRepository SuperHeroTeamService, IMapper Mapper)
        {
            _SuperHeroTeamService = SuperHeroTeamService;
            _Mapper = Mapper;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var group = await _SuperHeroTeamService.GetById(Id);
            if (group is null)
                return NotFound();
            var dto_group = _Mapper.Map<SuperHeroTeamDTO>(group);
            return Ok(dto_group);
        }

        [HttpPost("add/{heroId}")]
        public async Task<IActionResult> AddHeroInGroup(int heroId, [FromBody]AddHeroInGroupDTO group)
        {
            if (group is null)
                return NotFound();
            var result = await _SuperHeroTeamService.AddHeroInGroup(heroId, group);
            return Ok(result);
        }
    }
}
