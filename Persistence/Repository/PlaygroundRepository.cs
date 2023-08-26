using soulsync.Application.Interfaces;
using soulsync.Domain;


namespace soulsync.Persistence.Repository
{
    public class PlaygroundRepository : Repository<Playground>, IPlaygroundRepository
    {
        public PlaygroundRepository(AppDbContext context) : base(context)
        {

        }

       
    }

}
