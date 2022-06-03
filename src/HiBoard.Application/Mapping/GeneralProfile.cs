using AutoMapper;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;

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
        
        CreateMap<Template, TemplateDto>();
        CreateMap<TemplateDto, Template>();
    }
}