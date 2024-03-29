﻿using Application.Exceptions;
using Domain.Entities.Models;
using Domain.Modules;
using Infrastructure.EF.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Service
{
    public class AuthenticationModule : IAuthenticationModule<UserApplication>
    {
        private readonly ExpenseDbContext _context;

        private readonly IAuthenticationManagerService<UserApplication> _authenticationManager;

        private readonly IPasswordHasher<UserApplication> _passwordHasher;

        public AuthenticationModule(ExpenseDbContext context, IAuthenticationManagerService<UserApplication> authenticationManager, IPasswordHasher<UserApplication> passwordHasher)
        {
            _context = context;
            _authenticationManager = authenticationManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserApplication> SignIn(UserApplication model)
        {
            if (model is null)
                throw new BusinessException("User credentials can not be empty.", 401);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == model.Name);

            if(user == null)
                throw new NotFoundException("User not found.");

            var verify = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

            if (verify == PasswordVerificationResult.Failed)
                throw new BusinessException("Invalid password.", 404);

            return user;
        }

        public async Task<UserApplication> Register(UserApplication model)
        {
            if (model is null)
                throw new BusinessException("User credentials can not be empty.", 401);

            var existedUser = await _authenticationManager.FindUserByName(model.Name);

            if (existedUser != null)
                throw new BusinessException("User with the given credentials exists.", 409);

            model.CreatedDate = DateTime.Now;

            var hashedPasswdord = _passwordHasher.HashPassword(model, model.Password);

            model.Password = hashedPasswdord;

            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
