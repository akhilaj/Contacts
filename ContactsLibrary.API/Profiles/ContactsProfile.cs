using AutoMapper;
using ContactLibrary.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactLibrary.API.Entities;
using ContactsLibrary.API.Helpers;

namespace ContactLibrary.API.Profiles
{
    public class ContactsProfile : Profile
    {
        public ContactsProfile()
        {
            CreateMap<StatusCode, string>().ConvertUsing(x => EnumExtensionMethods.GetDescription(x));
            CreateMap<string, StatusCode>().ConvertUsing(new StringToStatusCodeConverter());

            CreateMap<Entities.Contact, Models.ContactDto>()
                .ForMember(
                    dest => dest.Name, 
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))                
                .ForMember(
                    dest => dest.Age, 
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));

            CreateMap<Models.ContactForCreationDto, Entities.Contact>();
        }

        private class StringToStatusCodeConverter : ITypeConverter<string, StatusCode>
        {
            private Dictionary<string, StatusCode> map = new Dictionary<string, StatusCode>
            {
                {"Active", StatusCode.Active},
                {"Inactive", StatusCode.Inactive }
                
            };
            public StatusCode Convert(string source, StatusCode destination, ResolutionContext context)
            {
                return map.GetValueOrDefault(source);
            }
        }
    }
}
