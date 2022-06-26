using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            var heroes = new List<SuperHero>
            {
                new SuperHero { Id = 1, 
                    Name = "Spider Man", 
                    FirstName = "Peter", 
                    LastName = "Parker", 
                    Place = "New York City"
                }
            };

            return Ok(heroes);
        }  
    }
}
