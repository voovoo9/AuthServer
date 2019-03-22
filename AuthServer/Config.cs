using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "Aurelia")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "edeja",
                    AllowedGrantTypes =GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("edeja123".Sha256())
                    },
                    AllowedScopes = {"api1"},
                    AllowOfflineAccess = true
                }
            };
        }
    }
}
