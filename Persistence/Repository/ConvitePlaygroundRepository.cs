using Microsoft.EntityFrameworkCore;
using soulsync.Application.Interfaces;
using soulsync.Domain;


namespace soulsync.Persistence.Repository
{
    public class ConvitePlaygroundRepository : Repository<ConvitePlayground>, IConvitePlaygroundRepository
    {
        private readonly AppDbContext _context;
        public ConvitePlaygroundRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConvitePlayground>> GetConvitesPorPlayground(int playgroundId)
        {
            var convites = await _context.ConvitesPlayground
                .Where(convite => convite.PlaygroundId == playgroundId)
                .ToListAsync();
            return convites;
        }
        public async Task<ConvitePlayground> CriarConvite(int playgroundId, int usuarioId)
        {
            string codigoConvite = GerarCodigoConviteNovoEUnico();

            var novoConvite = new ConvitePlayground()
            {

                PlaygroundId = playgroundId,
                UsuarioId = usuarioId,
                Codigo =  codigoConvite

            };

            await _context.Set<ConvitePlayground>().AddAsync(novoConvite);
            await _context.SaveChangesAsync();

            return novoConvite;
        }
        public string GerarCodigoConviteNovoEUnico()
        {
            int tamanho = 10;
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(Enumerable.Repeat(caracteres, tamanho)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Verifica se o código gerado já existe no banco de dados
            while (_context.ConvitesPlayground.Any(c => c.Codigo == code))
            {
                code = new string(Enumerable.Repeat(caracteres, tamanho)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            return code;
        }
    }

}
