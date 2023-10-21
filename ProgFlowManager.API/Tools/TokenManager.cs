using Microsoft.IdentityModel.Tokens;
using ProgFlowManager.DAL.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace ProgFlowManager.API.Tools
{
    public class TokenManager
    {
        public static string _secretKey = "Ghiuoshfu90yag7087y87YF9-s{Pfi0s-0oi09w- u322= i30=-r[p[oiq]-";

        public string GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_secretKey));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddDays(1),
                issuer: "monserveurapi.com",
                audience: "monsite.com"
                );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
