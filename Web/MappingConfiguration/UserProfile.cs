using Api.Models;
using Logic.Models;
using AutoMapper;

namespace Web.MappingConfiguration;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, RegisterRequest>();
        CreateMap<RegisterRequest, User>();
        CreateMap<LoginRequest, User>();
        CreateMap<User, LoginRequest>();
        CreateMap<User, ResponseDto>();
        CreateMap<ResponseDto, User>();
    }
}