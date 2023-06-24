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

        public async ValueTask ChangePassword(ChangePasswordRequest model)
        {
            var request = _mapper.Map<ChangePasswordModel>(model);

            await _accountModule.ChangePassword(request);
        }

        public async ValueTask DeleteAccount()
        {
           await _accountModule.DeleteAccount();
        }
    }
}
