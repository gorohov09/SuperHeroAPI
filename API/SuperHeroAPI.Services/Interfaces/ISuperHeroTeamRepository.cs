using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.Services.Interfaces
{
    public interface ISuperHeroTeamRepository
    {
        Task<SuperHeroTeam> GetById(int id);

    }
}
