using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trilhas.Models;
using TrilhasMicroservice.Repositories;

namespace Instyga.Trilhas.Services
{
    public class TrilhaService : ITrilhaService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrilhaService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public (bool contemErro, string mensagem) ValidarTrilha(Trilha trilha)
        {
            if (string.IsNullOrEmpty(trilha.Nome))
                return (true, "Nome não pode ser nulo ou vazio.");

            var exists = unitOfWork.Trilhas.Where(x => x.Nome == trilha.Nome).Any();
            if (exists)
                return (true, "Nome não pode ser duplicado.");

            return (false, "");
        }

        public async Task<ServiceResult<Trilha>> Incluir(Trilha trilha)
        {
            if (trilha.Id != default(int))
                return ServiceResult<Trilha>.Error("Caso deseja salvar uma entidade não especifique um ID.");

            var validacao = ValidarTrilha(trilha);

            if(validacao.contemErro)
                return ServiceResult<Trilha>.Error(validacao.mensagem);

            unitOfWork.Add(trilha);
            await unitOfWork.Commit();

           return ServiceResult<Trilha>.Ok($"Trilha {trilha.Nome} criada com sucesso.", trilha);
        }

        public async Task<ServiceResult<Trilha>> Atualizar(int id, Trilha trilha)
        {
            trilha.Id = id;

            var validacao = ValidarTrilha(trilha);

            if (validacao.contemErro)
                return ServiceResult<Trilha>.Error(validacao.mensagem);

            unitOfWork.MarkAsModified(trilha);
            try
            {
                await unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return ServiceResult<Trilha>.Error($"Não foi possivel atualizar. {ex.Message}");
            }

            return ServiceResult<Trilha>.Ok($"Trilha {trilha.Nome} criada com sucesso.", trilha);
        }

        private bool TrilhaExists(int id)
        {
            return unitOfWork.Trilhas.Any(e => e.Id == id);
        }
    }
}
