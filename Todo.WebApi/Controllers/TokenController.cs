using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Todo.WebApi.Dtos;
using Todo.WebApi.Models;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private TodoDbContext _context;
        private IConfiguration _configuration;

        public TokenController(IConfiguration configuration, TodoDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Users request)
        {
            if (ModelState.IsValid)
            {
                Models.Users user = _context.Users.FirstOrDefault(p =>
                    p.UserName == request.UserName && p.Password == request.Password);
                if (user == null)
                {
                    return Unauthorized();
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Issuer"], //appsettings.json içerisinde bulunan issuer değeri
                    audience: _configuration["Audience"], //appsettings.json içerisinde bulunan audince değeri
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(30), // 30 gün geçerli olacak
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                _configuration["SigningKey"])), //appsettings.json içerisinde bulunan signingkey değeri
                        SecurityAlgorithms.HmacSha256)
                );

                return Ok(new TokenResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    User =  new UserResponseModel
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Id = user.Id
                    }
                });
            }

            return BadRequest();
        }
    }
}