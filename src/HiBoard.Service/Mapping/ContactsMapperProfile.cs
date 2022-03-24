using AutoMapper;
using HiBoard.Service.Resources;

namespace HiBoard.Service.Mapping;

public class ContactsMapperProfile : Profile
{
    public ContactsMapperProfile()
    {
        CreateMap<Contact, ContactResource>();
        CreateMap<ContactResource, Contact>();
    }
}
