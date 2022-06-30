using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.Services.Interfaces
{
    public interface ISuperHeroTeamRepository
    {
        Task<SuperHeroTeam> GetById(int id);

        Task<bool> AddHeroInGroup(int heroId, AddHeroInGroupDTO group);

        Task<bool> DeleteHeroInGroup(int heroId);
    }
}
