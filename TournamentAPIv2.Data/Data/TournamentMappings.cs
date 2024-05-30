using AutoMapper;
using TournamentAPIv2.Core.Entities;
using TournamentAPIv2.Core.Dto;

namespace TournamentAPIv2.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDTO>().ReverseMap();
            CreateMap<Game, GameDTO>().ReverseMap();
        }
    }
}
