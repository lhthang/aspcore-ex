using cinema_core.Models;
using cinema_core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cinema_core.Utils.JWT
{
    public interface IJwtToken
    {
        ICollection<Claim> GetValidClaims(User user, List<Role> role);
        JwtSecurityToken GenerateToken(IUserRepository repository, User users);
    }
}
