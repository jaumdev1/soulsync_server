using Microsoft.AspNetCore.Mvc;
using soulsync.Application.Interfaces;
using soulsync.Application.Services;
using soulsync.Domain;
using soulsync.Presentation.Models;
namespace soulsync.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioRepository)
        {
            _usuarioService = usuarioRepository;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int novoUsuarioId = await _usuarioService.AddUsuario(model.Nome, model.Email, model.Senha);

            return Ok(new { Message = "Usuário criado com sucesso.", UsuarioId = novoUsuarioId });
        }


    }
}
