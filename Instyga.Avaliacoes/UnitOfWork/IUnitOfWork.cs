using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avaliacoes.Models;

namespace AvaliacoesMicroservice.Repositories
{
    public interface IUnitOfWork
    {
        public IQueryable<Avaliacao> Avaliacoes { get; }
        void Add(Avaliacao avaliacao);
        void Remove(Avaliacao avaliacao);
        public Task<int> Commit();
        void MarkAsModified<T>(T entity);
    }

    public class AvaliacaoMicroserviceUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoMicroserviceUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Avaliacao> Avaliacoes => _context.Avaliacoes;

        public void Add(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Add(avaliacao);
        }

        public void Remove(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Remove(avaliacao);
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
