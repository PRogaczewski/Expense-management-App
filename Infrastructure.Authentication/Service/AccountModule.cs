using Application.Authentication.IServices;
using Application.Exceptions;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Service
{
    public class AccountModule : IAccountModule
    {
        private readonly ExpenseDbContext _context;

        private readonly IAuthenticationManagerService<UserApplication> _authenticationManager;

        private readonly IPasswordHasher<UserApplication> _passwordHasher;

        private readonly IUserContextModule _userContext;

        public AccountModule(IAuthenticationManagerService<UserApplication> authenticationManager, ExpenseDbContext context, IPasswordHasher<UserApplication> passwordHasher, IUserContextModule userContext)
        {
            _authenticationManager = authenticationManager;
            _context = context;
            _passwordHasher = passwordHasher;
            _userContext = userContext;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var userAcc = await _authenticationManager.FindUserByName(model.Name);

            if (userAcc == null)
                throw new NotFoundException("User not found.");

            var verify = _passwordHasher.VerifyHashedPassword(userAcc, userAcc.Password, model.OldPassword);

            if (verify == PasswordVerificationResult.Failed)
                throw new BusinessException("Invalid password.", 404);

            var hashedPassword = _passwordHasher.HashPassword(userAcc, model.NewPassword);

            userAcc.Password = hashedPassword;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAccount()
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var userAcc = _context.Users.FirstOrDefault(u => u.Id == userId);

            if(userAcc == null)
                throw new NotFoundException("User not found.");

             _context.Users.Remove(userAcc);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
