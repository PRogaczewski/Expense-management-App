using Application.Authentication.IServices;
using Domain.Entities.Models;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Authentication.Service
{
    public class AuthenticationManagerService : IAuthenticationManagerService<UserApplication>
    {
        private readonly ExpenseDbContext _context;

        public AuthenticationManagerService(ExpenseDbContext context)
        {
            _context = context;
        }

        public bool Succeeded { get; set; }

        public async Task<UserApplication> FindUserByName(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

            return user;
        }

        public List<Claim> GetClaims(UserApplication user)
        {
            Claim claim1 = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            Claim claim2 = new Claim(ClaimTypes.Name, user.Name);
            Claim claim3 = new Claim(ClaimTypes.Role, "User");

            return new List<Claim> { claim1, claim2, claim3 };
        }
    }
}
