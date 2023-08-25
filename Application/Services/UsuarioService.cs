using soulsync.Application.Interfaces;
using soulsync.Domain;
using BCrypt.Net;
using soulsync.Persistence.Repository;
using soulsync.Persistence;
using Microsoft.EntityFrameworkCore;

namespace soulsync.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AppDbContext _context;
        public UsuarioService(IUsuarioRepository usuarioRepository,  AppDbContext context)
        {
            _usuarioRepository = usuarioRepository;
            _context = context;

        }

        public async Task<int> AddUsuario(string nome, string email, string senha)
        {
            
            var hashedSenha = BCrypt.Net.BCrypt.HashPassword(senha);
            var newUsuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = hashedSenha
            };
           
            await _usuarioRepository.Add(newUsuario);

            
            return newUsuario.Id;
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

    }
}
