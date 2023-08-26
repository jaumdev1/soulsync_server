using soulsync.Application.Interfaces;
using soulsync.Domain;
using soulsync.Persistence;

namespace soulsync.Application.Services
{
    public class UsuarioPlaygroundService
    {
        private readonly IRepository<Playground> _usuarioPlaygroundRepository;
        private readonly AppDbContext _context;
        public UsuarioPlaygroundService(IRepository<Playground> playgroundRepository, AppDbContext context)
        {
            _usuarioPlaygroundRepository = playgroundRepository;
            _context = context;
        }
    }
}
