using Application.Authentication.IServices;
using Application.Authentication.Models;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IAuthenticationModule<UserApplication> _authenticationModule;

        private readonly WebToken _webToken;

        private readonly IAuthenticationManagerService<UserApplication> _authenticationManager;

        private readonly IMapper _mapper;

        public AuthenticationService(IAuthenticationModule<UserApplication> authenticationModule, IMapper mapper, IOptions<WebToken> webToken, IAuthenticationManagerService<UserApplication> authenticationManager)
        {
            _authenticationModule = authenticationModule;
            _mapper = mapper;
            _webToken = webToken.Value;
            _authenticationManager = authenticationManager;
        }

        public async Task<AuthenticationResponse> SignIn(AuthenticationRequest model)
        {
            var user = _mapper.Map<UserApplication>(model);

            var response = await _authenticationModule.SignIn(user);

            if (response == null)
                throw new BusinessException("Something went wrong...", 404);

            var token = GenerateToken(response);

            return new AuthenticationResponse() { Name = response.Name, Token = token };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest model)
        {
            var user = _mapper.Map<UserApplication>(model);

            var response = await _authenticationModule.Register(user);

            if(response == null)
                throw new BusinessException("Something went wrong...", 404);

            var token = GenerateToken(response);

            return new RegistrationResponse() { Name = response.Name, Token = token };
        }

        private string GenerateToken(UserApplication user)
        {
            var claims = _authenticationManager.GetClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_webToken.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_webToken.ExpiresInMinutes);

            var token = new JwtSecurityToken(_webToken.Issuer, _webToken.Audience, claims, expires: expires, signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
