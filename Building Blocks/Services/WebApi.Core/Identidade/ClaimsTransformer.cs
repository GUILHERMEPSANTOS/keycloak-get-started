using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace WebApi.Core.Identidade
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private string _audience;

        public ClaimsTransformer(string audience)
        {
            _audience = audience;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;

            var authenticated = claimsIdentity.IsAuthenticated;

            if (!authenticated)
            {
                return Task.FromResult(principal);
            }

            var resourceAccessValue = principal.FindFirst("resource_access")?.Value;

            if (string.IsNullOrEmpty(resourceAccessValue))
            {
                return Task.FromResult(principal);
            }

            using var resourceAccess = JsonDocument.Parse(resourceAccessValue);

            var clientRoles = resourceAccess
                   .RootElement
                   .GetProperty(_audience)
                   .GetProperty("roles");

            foreach (var role in clientRoles.EnumerateArray())
            {
                var value = role.GetString();

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, value));
            }

            return Task.FromResult(principal);
        }
    }
}
