using System.Threading.Tasks;
using Trilhas.Models;

namespace Instyga.Trilhas.Services
{
    public interface ITrilhaService
    {
        Task<ServiceResult<Trilha>> Atualizar(int id, Trilha trilha);
        Task<ServiceResult<Trilha>> Incluir(Trilha trilha);
    }
}