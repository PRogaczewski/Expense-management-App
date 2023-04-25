using Application.Authentication.Models;

namespace Application.Authentication.IServices
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> SignIn(AuthenticationRequest model);

        Task<RegistrationResponse> Register(RegistrationRequest model);
    }
}
