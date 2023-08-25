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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace soulsync.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaygroundController : ControllerBase
    {
        private readonly PlaygroundService _playgroundService;

        public PlaygroundController(PlaygroundService usuarioRepository)
        {
            _playgroundService = usuarioRepository;
        }

        [HttpPost("criar-playground")]
        [Authorize] // Apenas usuários autenticados podem acessar esta rota
        public async Task<IActionResult> CreatePlayground([FromBody] PlaygroundModel model)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                // Seu código para criar o playground aqui, usando o _playgroundService

                await _playgroundService.AddPlaygroundWithAdministradores(model.Nome, model.Descricao, Convert.ToInt32(userId), model.OutrosAdministradoresIds);

                return Ok(new { Message = "Playground criado com sucesso." });
            }
            catch (Exception ex)
            {
                // Lida com erros e retorna um BadRequest, por exemplo
                return BadRequest(new { Message = "Erro ao criar playground.", Error = ex.Message });
            }
        }



    }
}
