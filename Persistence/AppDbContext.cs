using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using soulsync.Domain;

namespace soulsync.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Playground> Playgrounds { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<UsuarioPlayground> UsuariosPlayground { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
