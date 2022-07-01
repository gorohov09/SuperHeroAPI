using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SuperHeroAPI.DAL.Context;
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

        public async Task<SuperHeroTeam> GetById(int id)
        {
            var group = await _Context.SuperHeroTeams
                .FirstOrDefaultAsync(t => t.Id == id);

            return group;   
        }
    }
}
