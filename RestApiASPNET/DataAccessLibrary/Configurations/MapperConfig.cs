using AutoMapper;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Configurations;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        CreateMap<TeamDtoAdmin, Team>().ReverseMap();
        CreateMap<UserDtoAdmin, User>().ReverseMap();
        CreateMap<UserDtoPublic, User>().ReverseMap();
        CreateMap<MissionDtoAdmin, Mission>().ReverseMap();
        CreateMap<Event, EventDtoAdmin>()
            .ForMember(a => a.Missions, opt => opt
            .MapFrom(src => src.EventMissions.Select(a => a.Mission)
                .ToList())).ReverseMap();
        CreateMap<EventMissionDto, EventMission>().ReverseMap();
        // CreateMap<List<UserDtoAdmin>, List<User>>().ReverseMap();

    }
}