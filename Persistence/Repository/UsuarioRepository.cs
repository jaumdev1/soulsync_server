using soulsync.Application.Interfaces;
using soulsync.Domain;

namespace soulsync.Persistence.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {

        }

      
    }

}
