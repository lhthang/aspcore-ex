using cinema_core.Models;
using cinema_core.Repositories;
using cinema_core.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cinema_core.Utils.JWT
{
    public class JwtToken : IJwtToken
    {
        public JwtSecurityToken GenerateToken(IUserRepository repository, User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MyConfig.Get("jwt:SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(MyConfig.Get("jwt:ExpireDays")));

            List<Role> roles = repository.GetRolesOfUser(user.Id).ToList();
            // Create the JWT security token and encode it.
            var token = new JwtSecurityToken(
                issuer: MyConfig.Get("jwt:Issuer"),
                audience: MyConfig.Get("jwt:Audience"),
                claims: GetValidClaims(user, roles),
                expires: expires,
                signingCredentials: creds);
            return token;
        }


        public ICollection<Claim> GetValidClaims(User user, List<Role> roles)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("fullName", user.FullName.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var userRole in roles)
            {
                claims.Add(new Claim("role", userRole.Name));
            }
            return claims;
        }


    }
}
