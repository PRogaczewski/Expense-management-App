using Domain.Entities.Models;
using System.Security.Claims;

namespace Application.Authentication.IServices
{
    public interface IAuthenticationManagerService<T> where T : class
    {
        bool Succeeded { get; protected set; }

        Task<T> FindUserByName(string name);

        List<Claim> GetClaims(T user);
    }
}
