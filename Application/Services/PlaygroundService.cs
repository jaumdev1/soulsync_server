using Microsoft.EntityFrameworkCore;
using soulsync.Application.Interfaces;
using soulsync.Domain;
using soulsync.Persistence;

namespace soulsync.Application.Services
{
    public class PlaygroundService
    {
        private readonly IRepository<Playground> _playgroundRepository;
        private readonly AppDbContext _context;
        public PlaygroundService(IRepository<Playground> playgroundRepository, AppDbContext context)
        {
            _playgroundRepository = playgroundRepository;
            _context = context;
        }

        public async Task<IEnumerable<Playground>> GetAllPlaygrounds()
        {
            return await _playgroundRepository.GetAll();
        }

        public async Task<int> CriarPlaygroundComAdministradores(string nome, string descricao, int administradorPrincipalId, List<int> outrosAdministradoresIds)
        {
            // Realizar validações, lógica de negócios, etc., se necessário

            var administradores = new List<Administrador>();

            var usuariosPlayground = new List<UsuarioPlayground>();
            foreach (int adminId in outrosAdministradoresIds)
            {
                if(adminId !=0)
                administradores.Add(new Administrador { UsuarioId = adminId });
            }

            var newPlayground = new Playground
            {
                Nome = nome,
                Descricao = descricao,
                AdministradorPrincipalId = administradorPrincipalId,
                Administradores = administradores,
                UsuariosPlayground = usuariosPlayground
            };

            await _playgroundRepository.Add(newPlayground);

            
            return newPlayground.Id;
        }


        public async Task<bool> UsuarioAdminDoPlayGround(int usuarioAdminId, int playgroundId)
        {
          var playground = _context.Playgrounds.AsQueryable().Where(p =>p.Id == playgroundId).SingleOrDefault();

            if (playground.AdministradorPrincipalId == usuarioAdminId)
                return true;

            return false;

        }


       
    }
}
