using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SPCLCTAPI.ApiModels;
using SPCLCTAPI.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPCLTCAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _config = configuration;
        }

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel registration)
        {
            var newUser = new User
            {
                UserName = registration.Email,
                Email = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName
            };
            var result = await _userManager.CreateAsync(newUser, registration.Password);
            if (result.Succeeded)
            {
                return Ok(newUser.ToApiModel());
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        // POST api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUserAsync(login.Email, login.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, email = login.Email });
            }

            return response;
        }

        private string GenerateJSONWebToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var roles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
             };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);
            return tokenHandler.WriteToken(token);
        }

        private async Task<User> AuthenticateUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }
    }
}
