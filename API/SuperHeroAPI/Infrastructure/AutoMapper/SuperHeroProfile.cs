using AutoMapper;
using SuperHeroAPI.Domain.DTO;
using SuperHeroAPI.Domain.Entities;

namespace SuperHeroAPI.Infrastructure.AutoMapper
{
    public class SuperHeroProfile : Profile
    {
        public SuperHeroProfile()
        {
            CreateMap<SuperHero, SuperHeroDTO>()
                .ReverseMap();

            CreateMap<Ability, AbilityDTO>()
                .ReverseMap();

            CreateMap<SuperHeroTeam, SuperHeroTeamDTO>()
                .ReverseMap();
        }
    }
}
