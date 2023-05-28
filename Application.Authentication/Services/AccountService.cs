using Application.Authentication.Models;
using AutoMapper;
using Domain.Modules;
using Domain.ValueObjects;

namespace Application.Authentication.IServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountModule _accountModule;

        private readonly IMapper _mapper;

        public AccountService(IAccountModule accountModule, IMapper mapper)
        {
            _accountModule = accountModule;
            _mapper = mapper;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest model)
        {
            var request = _mapper.Map<ChangePasswordModel>(model);

            return await _accountModule.ChangePassword(request);
        }

        public async Task<bool> DeleteAccount()
        {
           return await _accountModule.DeleteAccount();
        }
    }
}
