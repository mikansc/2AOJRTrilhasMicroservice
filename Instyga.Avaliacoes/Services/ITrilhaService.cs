using System.Threading.Tasks;
using Avaliacoes.Models;

namespace Instyga.Avaliacoes.Services
{
    public interface IAvaliacoeservice
    {
        Task<ServiceResult<Avaliacao>> Atualizar(int id, Avaliacao Avaliacao);
        Task<ServiceResult<Avaliacao>> Incluir(Avaliacao Avaliacao);
    }
}