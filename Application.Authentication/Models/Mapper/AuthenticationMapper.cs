using AutoMapper;
using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Application.Authentication.Models.Mapper
{
    public class AuthenticationMapper : Profile
    {
        public AuthenticationMapper()
        {
            CreateMap<RegistrationRequest, UserApplication>();

            CreateMap<AuthenticationRequest, UserApplication>();

            CreateMap<ChangePasswordRequest, ChangePasswordModel>()
                .ForMember(u=>u.OldPassword, e=>e.MapFrom(o=>o.Password));
        }
    }
}
