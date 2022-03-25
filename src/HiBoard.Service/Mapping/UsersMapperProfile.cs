using AutoMapper;
using HiBoard.Models;
using HiBoard.Service.Resources;

namespace HiBoard.Service.Mapping;

public class UsersMapperProfile : Profile
{
    public UsersMapperProfile()
    {
        CreateMap<User, UserResource>();
        CreateMap<UserResource, User>();
    }
}
