using Microsoft.EntityFrameworkCore;
using soulsync.Application.Interfaces;
using soulsync.Domain;
using soulsync.Persistence;

namespace soulsync.Application.Services
{
    public class ConvitePlaygroundService
    {
        private readonly IConvitePlaygroundRepository _convitePlaygroundRepository;
        public ConvitePlaygroundService(IConvitePlaygroundRepository convitePlaygroundRepository)
        {
            _convitePlaygroundRepository = convitePlaygroundRepository;
         
        }
        public async Task<ConvitePlayground> CriarConvitePlayground(int playgroundId, int usuarioId)
        {
            var convite = await _convitePlaygroundRepository.CriarConvite(playgroundId, usuarioId);
            
            return convite;

        }
    }
}
