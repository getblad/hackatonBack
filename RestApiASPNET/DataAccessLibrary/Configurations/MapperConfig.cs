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
        // CreateMap<List<UserDtoAdmin>, List<User>>().ReverseMap();

    }
}