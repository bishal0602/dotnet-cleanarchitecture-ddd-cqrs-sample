using Books.BlazorWasm.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Books.BlazorWasm.Auth
{
    public static class AuthUtilities
    {
        public const string LocalStorageTokenKey = "bookswasm:token";
        public static ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsIdentity identity = new();
            if (tokenHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Books.API");
            }
            return new ClaimsPrincipal(identity);
        }
        public static ClaimsPrincipal CreateClaimsPrincipalFromUser(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim (JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim (JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim (JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim (JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
            };
            claims = claims.Concat(user.Roles.Select(r => new Claim(ClaimTypes.Role, r))).ToArray();

            return new(new ClaimsIdentity(claims, "Books.API"));
        }
        public static User CreateUserFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            Claim? userIdClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub);
            Claim? userNameClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.UniqueName);
            Claim? firstNameClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.GivenName);
            Claim? lastNameClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.FamilyName);
            Claim? emailClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Email);
            //IEnumerable<Claim> roleClaims = claimsPrincipal.FindAll(ClaimTypes.Role);

            User user = new()
            {
                UserId = userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty,
                UserName = userNameClaim != null ? userNameClaim.Value : string.Empty,
                FirstName = firstNameClaim != null ? firstNameClaim.Value : string.Empty,
                LastName = lastNameClaim != null ? lastNameClaim.Value : string.Empty,
                Email = emailClaim != null ? emailClaim.Value : string.Empty,
                //Roles = roleClaims != null ? roleClaims.Select(c => c.Value).ToList() : new List<string>()
            };
            return user;
        }
    }
}
