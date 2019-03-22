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
using Serilog;

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
            Log.Information("Started getting profile data.");

            try
            {
                var subjectId = context.Subject.GetSubjectId();
                var user = _authRepository.GetUserById(subjectId);

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, user.Id)
                };
                context.IssuedClaims = claims;

                Log.Information("Finished getting profile data.");

                return Task.FromResult(0);
            }
            catch (Exception)
            {
                Log.Information("Error occured while getting profile data.");

                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            Log.Information("Started checking user IsActive status.");

            //var user = _authRepository.GetUserByUsername(context.Subject.GetDisplayName());
            var user = _authRepository.GetUserById(context.Subject.GetSubjectId());
            context.IsActive = (user != null) && user.IsActive;

            Log.Information("Finished checking user IsActive status.");

            return Task.FromResult(0);
        }
    }
}
