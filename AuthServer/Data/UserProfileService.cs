using AuthServer.Data;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityModel;

namespace AuthServer
{
    public class UserProfileService : IProfileService
    {
        IAuthRepository _authRepository;

        public UserProfileService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = _authRepository.GetUserById(subjectId);

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString())
                };
                context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _authRepository.GetUserByUsername(context.Subject.GetDisplayName());
            var user = _authRepository.GetUserById(context.Subject.GetSubjectId());
            context.IsActive = (user != null) && user.Active;
            return Task.FromResult(0);
        }
    }
}
