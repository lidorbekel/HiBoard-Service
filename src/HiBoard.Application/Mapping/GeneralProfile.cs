using AutoMapper;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;

namespace HiBoard.Application.Mapping;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dto => dto.Id, e => e.MapFrom(user => user.Id))
            .ForMember(dto => dto.FirstName, e => e.MapFrom(user => user.FirstName))
            .ForMember(dto => dto.LastName, e => e.MapFrom(user => user.LastName))
            .ForMember(dto => dto.UserName, e => e.MapFrom(user => user.UserName))
            .ForMember(dto => dto.Department, e => e.MapFrom(user => user.Department))
            .ForMember(dto => dto.Role, e => e.MapFrom(user => user.Role));

        CreateMap<Mission, MissionDto>()
            .ForMember(dto => dto.Id, e => e.MapFrom(mission => mission.Id))
            .ForMember(dto => dto.Title, e => e.MapFrom(mission => mission.Title))
            .ForMember(dto => dto.Description, e => e.MapFrom(mission => mission.Description))
            .ForMember(dto => dto.Status, e => e.MapFrom(mission => mission.Status))
            .ForMember(dto => dto.Tag, e => e.MapFrom(mission => mission.Tag))
            .ForMember(dto => dto.TimeEstimation, e => e.MapFrom(mission => mission.TimeEstimation))
            .ForMember(dto => dto.DependencyIds, e => e.MapFrom(mission => mission.DependencyIds));

    }
}