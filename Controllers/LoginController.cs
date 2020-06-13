using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using cinema_core.Form;
using cinema_core.Repositories.Interfaces;
using cinema_core.Utils.JWT;
using cinema_core.Utils.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cinema_core.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : Controller
    {
        private IUserRepository userRepository;

        private PasswordHasher passwordHasher;

        private JwtToken jwtToken;

        public LoginController(IUserRepository repository)
        {
            userRepository = repository;
            passwordHasher = new PasswordHasher();
            jwtToken = new JwtToken();
        }
        [HttpPost()]
        public IActionResult PostUser([FromBody] UserRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var found = userRepository.GetUserByUsername(user.Username);

            if (found == null)
                return Unauthorized();

            if (!passwordHasher.PasswordMatches(user.Password, found.Password))
                return Unauthorized();

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken.GenerateToken(userRepository, found));
            return Content("{ \"token\":\"" + token + "\"}", "application/json");
        }
    }
}