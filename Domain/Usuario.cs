using Microsoft.AspNetCore.Identity;

namespace soulsync.Domain
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<UsuarioPlayground> UsuariosPlayground { get; set; }
        public List<Administrador> Administradores { get; set; }
    }
}
