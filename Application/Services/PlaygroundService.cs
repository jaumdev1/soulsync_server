using soulsync.Application.Interfaces;
using soulsync.Domain;

namespace soulsync.Application.Services
{
    public class PlaygroundService
    {
        private readonly IRepository<Playground> _playgroundRepository;

        public PlaygroundService(IRepository<Playground> playgroundRepository)
        {
            _playgroundRepository = playgroundRepository;
        }

        public async Task<IEnumerable<Playground>> GetAllPlaygrounds()
        {
            return await _playgroundRepository.GetAll();
        }
        public async Task<int> AddPlaygroundWithAdministradores(string nome, string descricao, int administradorPrincipalId, List<int> outrosAdministradoresIds)
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

            // O ID será atualizado após a inserção
            return newPlayground.Id;
        }
    }
}
