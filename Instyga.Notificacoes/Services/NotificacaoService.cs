using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notificacoes.Models;
using NotificacoesMicroservice.Repositories;

namespace Instyga.Notificacoes.Services
{
    public class NotificacoesService : INotificacoesService
    {
        private readonly IUnitOfWork unitOfWork;

        public NotificacoesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public (bool contemErro, string mensagem) ValidarNotificacao(Notificacao Notificacao)
        {
            if (string.IsNullOrEmpty(Notificacao.Mensagem))
                return (true, "Uma notificação deve conter uma mensagem");

            if (string.IsNullOrEmpty(Notificacao.Trilha))
                return (true, "Uma notificação deve estar associada a uma trilha.");

            return (false, "");
        }

        public async Task<ServiceResult<Notificacao>> Incluir(Notificacao Notificacao)
        {
            if (Notificacao.Id != default(int))
                return ServiceResult<Notificacao>.Error("Caso deseja salvar uma entidade não especifique um ID.");

            var validacao = ValidarNotificacao(Notificacao);

            if(validacao.contemErro)
                return ServiceResult<Notificacao>.Error(validacao.mensagem);

            unitOfWork.Add(Notificacao);
            await unitOfWork.Commit();

           return ServiceResult<Notificacao>.Ok($"Notificacao criada com sucesso.", Notificacao);
        }


        private bool NotificacaoExists(int id)
        {
            return unitOfWork.Notificacoes.Any(e => e.Id == id);
        }
    }
}
