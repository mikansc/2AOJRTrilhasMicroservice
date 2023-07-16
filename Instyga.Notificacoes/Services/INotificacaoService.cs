using System.Threading.Tasks;
using Notificacoes.Models;

namespace Instyga.Notificacoes.Services
{
    public interface INotificacoesService
    {
        Task<ServiceResult<Notificacao>> Incluir(Notificacao Notificacao);
    }
}