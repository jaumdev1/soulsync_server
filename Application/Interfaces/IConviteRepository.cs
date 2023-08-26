using soulsync.Domain;

namespace soulsync.Application.Interfaces
{
    public interface IConvitePlaygroundRepository : IRepository<ConvitePlayground>
    {
         Task<IEnumerable<ConvitePlayground>> GetConvitesPorPlayground(int playgroundId);
        Task<ConvitePlayground> CriarConvite(int playgroundId, int usuarioId);
    }
}
