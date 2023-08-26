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
using soulsync.Persistence;

namespace soulsync.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaygroundController : ControllerBase
    {
        private readonly PlaygroundService _playgroundService;
        private readonly ConvitePlaygroundService _convitePlaygroundService;
        public PlaygroundController(PlaygroundService usuarioRepository, ConvitePlaygroundService convitePlaygroundService)
        {
            _playgroundService = usuarioRepository;
            _convitePlaygroundService = convitePlaygroundService;


        }

        [HttpPost("criarPlayground")]
        [Authorize] 
        public async Task<IActionResult> CriarPlayground([FromBody] PlaygroundModel model)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                // Seu código para criar o playground aqui, usando o _playgroundService

                await _playgroundService.CriarPlaygroundComAdministradores(model.Nome, model.Descricao, Convert.ToInt32(userId), model.OutrosAdministradoresIds);

                return Ok(new { Message = "Playground criado com sucesso." });
            }
            catch (Exception ex)
            {
                // Lida com erros e retorna um BadRequest, por exemplo
                return BadRequest(new { Message = "Erro ao criar playground.", Error = ex.Message });
            }
        }

        [HttpPost("criarConvitePlayground")]
        [Authorize]
        public async Task<IActionResult> CriarConvitePlayground([FromBody] PlaygroundModelInvite modelInvite)
        {
         
            try
            {
                var usuarioId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var isAdminDoPlayground = await _playgroundService.UsuarioAdminDoPlayGround(usuarioId, modelInvite.PlaygroundId);

                if (isAdminDoPlayground == false)
                    throw new Exception("Usuário sem permissão.");



              var convite =  await _convitePlaygroundService.CriarConvitePlayground(modelInvite.PlaygroundId, usuarioId);



                return Ok(new { Message = "Convite criado com sucesso." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erro ao Criar convite playground.", Error = ex.Message });
            }
        }




    }
}
