using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacoes.Models;
using AvaliacoesMicroservice.Repositories;

namespace Instyga.Avaliacoes.Services
{
    public class Avaliacoeservice : IAvaliacoeservice
    {
        private readonly IUnitOfWork unitOfWork;

        public Avaliacoeservice(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public (bool contemErro, string mensagem) ValidarAvaliacao(Avaliacao Avaliacao)
        {
            if (string.IsNullOrEmpty(Avaliacao.Nome))
                return (true, "Nome não pode ser nulo ou vazio.");

            if (string.IsNullOrEmpty(Avaliacao.NomeTrilha))
                return (true, "Uma avaliação deve estar associada a uma trilha.");

            var exists = unitOfWork.Avaliacoes.Where(x => x.Nome == Avaliacao.Nome).Any();
            if (exists)
                return (true, "Nome não pode ser duplicado.");

            return (false, "");
        }

        public async Task<ServiceResult<Avaliacao>> Incluir(Avaliacao Avaliacao)
        {
            if (Avaliacao.Id != default(int))
                return ServiceResult<Avaliacao>.Error("Caso deseja salvar uma entidade não especifique um ID.");

            var validacao = ValidarAvaliacao(Avaliacao);

            if(validacao.contemErro)
                return ServiceResult<Avaliacao>.Error(validacao.mensagem);

            unitOfWork.Add(Avaliacao);
            await unitOfWork.Commit();

           return ServiceResult<Avaliacao>.Ok($"Avaliacao {Avaliacao.Nome} criada com sucesso.", Avaliacao);
        }

        public async Task<ServiceResult<Avaliacao>> Atualizar(int id, Avaliacao Avaliacao)
        {
            Avaliacao.Id = id;

            var validacao = ValidarAvaliacao(Avaliacao);

            if (validacao.contemErro)
                return ServiceResult<Avaliacao>.Error(validacao.mensagem);

            unitOfWork.MarkAsModified(Avaliacao);
            try
            {
                await unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return ServiceResult<Avaliacao>.Error($"Não foi possivel atualizar. {ex.Message}");
            }

            return ServiceResult<Avaliacao>.Ok($"Avaliacao {Avaliacao.Nome} criada com sucesso.", Avaliacao);
        }

        private bool AvaliacaoExists(int id)
        {
            return unitOfWork.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
