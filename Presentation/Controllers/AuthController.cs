using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using soulsync.Domain;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using soulsync.Presentation.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using soulsync.Application.Interfaces;
using soulsync.Application.Services;
using System.Security.Claims;

namespace soulsync.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public AuthController(UsuarioService usuarioRepository)
        {
            _usuarioService = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _usuarioService.GetUsuarioByEmail(model.Email);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Senha, user.Senha)) // Verifica a senha com BCrypt
            {
                var chaveSecreta = configuration.GetSection("AppSettings")["ChaveSecreta"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "SOULSYNC",
                    audience: "SOULSYNC_CLIENT",
                    expires: DateTime.UtcNow.AddHours(24), // Define o tempo de expiração do token
                    signingCredentials: creds,
                     claims: new[]
                      {
                          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                      }
                );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized(new { Message = "Credenciais inválidas." });
        }



    }
}
