using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Data
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepository _authRepository;

        public ResourceOwnerPasswordValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if(_authRepository.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(_authRepository.GetUserByUsername(context.UserName).Id, "password", null, "local", null);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The user or passward is incorrect", null);
            }
            return Task.FromResult(context.Result);
        }
    }
}
