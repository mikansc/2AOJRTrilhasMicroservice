using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notificacoes.Models;

namespace NotificacoesMicroservice.Repositories
{
    public interface IUnitOfWork
    {
        public IQueryable<Notificacao> Notificacoes { get; }
        void Add(Notificacao Notificacao);
        void Remove(Notificacao Notificacao);
        public Task<int> Commit();
        void MarkAsModified<T>(T entity);
    }

    public class NotificacaoMicroserviceUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public NotificacaoMicroserviceUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Notificacao> Notificacoes => _context.Notificacoes;

        public void Add(Notificacao Notificacao)
        {
            _context.Notificacoes.Add(Notificacao);
        }

        public void Remove(Notificacao Notificacao)
        {
            _context.Notificacoes.Remove(Notificacao);
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void MarkAsModified<T>(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
