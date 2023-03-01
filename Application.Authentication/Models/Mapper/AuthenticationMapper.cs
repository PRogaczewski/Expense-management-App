using AutoMapper;
using Domain.Entities.Models;

namespace Application.Authentication.Models.Mapper
{
    public class AuthenticationMapper : Profile
    {
        public AuthenticationMapper()
        {
            CreateMap<RegistrationRequest, UserApplication>();

            CreateMap<AuthenticationRequest, UserApplication>();
        }
    }
}
