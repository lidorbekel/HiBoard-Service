using AutoMapper;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;

namespace HiBoard.Application.Mapping;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

        CreateMap<Activity, ActivityDto>();
        CreateMap<Activity, ActivityDto>();

        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();

        CreateMap<UserActivity, UserActivityDto>();
        CreateMap<UserActivityDto, UserActivity>();

        CreateMap<PatchUser, UserDto>();
        CreateMap<UserDto, PatchUser>();
    }
}