using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerWithAspIdAndEF
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {  
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource( "role", "Roles", claimTypes: new[]{ "role" })
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    UserClaims = { "role" }  
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {                
                new Client
                {
                    ClientId = "angular",
                    ClientName = "Angular Client",
                    AccessTokenLifetime = 60*60,
                    AllowedGrantTypes = GrantTypes.Implicit,                   
                    AlwaysSendClientClaims = true,                    
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = true,
                    ClientClaimsPrefix = "",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenType = AccessTokenType.Jwt,

                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200/" },

                    AllowedScopes =
                    {
                        "openid",
                        "profile",
                        "email",
                        "role",
                        "api1"
                    },
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:4200",
                        "http://localhost:5001",

                    }
                }
            };
        }
    }
}