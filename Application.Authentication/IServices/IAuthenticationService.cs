using Application.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.IServices
{
    public interface IAuthenticationService
    {
        AuthenticationResponse SignIn(AuthenticationRequest model);

        RegistrationResponse Register(RegistrationRequest model);
    }
}
