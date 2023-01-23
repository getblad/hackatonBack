using AutoMapper;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Configurations;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        CreateMap<EventUserDto, EventUser>().ReverseMap();
        CreateMap<TeamDtoAdmin, Team>().ReverseMap();
        CreateMap<UserDtoAdmin, User>().ReverseMap();
        CreateMap<UserDtoPublic, User>().ReverseMap();
        CreateMap<MissionDtoAdmin, Mission>().ReverseMap();
        CreateMap<Event, EventDtoAdmin>()
            .ForMember(a => a.Missions, opt => opt
            .MapFrom(src => src.EventMissions.Select(Func)
                .ToList())).ReverseMap();
        CreateMap<EventMissionDto, EventMission>().ReverseMap();
        // CreateMap<IGrouping<int, EventMission>, Event>().ForMember(dest => dest,
            // opt => 
                // opt.MapFrom(sourceMember => sourceMember.Select(a => a.Event).First()));
        // .ForMember(dest => dest.Missions);
        // CreateMap<List<EventMission>, List<EventDtoAdmin>>().ForMember(a => a.ForEach(a => a.Missions), opt => {})
        // CreateMap<List<UserDtoAdmin>, List<User>>().ReverseMap();

    }

    private static Mission Func(EventMission a)
    {
        a.Mission.MissionPoint = a.EventMissionPoint ?? a.Mission.MissionPoint;
        a.Mission.MissionLanguage = (a.EventMissionLanguage ?? a.Mission.MissionLanguage); 
        a.Mission.MissionExecutionTime = a.EventMissionExecutionTime ?? a.Mission.MissionExecutionTime; 
        a.Mission.MissionStepPointFine = a.EventMissionStepPointFine ?? a.Mission.MissionStepPointFine;
        a.Mission.MissionStepTimeFine = a.EventMissionStepTimeFine ?? a.Mission.MissionStepTimeFine;
        return a.Mission;
    }
}