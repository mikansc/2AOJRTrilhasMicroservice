using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace TrilhasMicroservice.Repositories
{
    public interface IUnitOfWork
    {
        public IQueryable<Avaliacao> Avaliacoes { get; }
        public IQueryable<Trilha> Trilhas { get; }

        //Operations
        void Add(Avaliacao avaliacao);
        void Add(Trilha avaliacao);
        void Remove(Avaliacao avaliacao);
        void Remove(Trilha avaliacao);

        public Task<int> Commit();
        void MarkAsModified<T>(T entity);
    }

    public class TrilhaMicroserviceUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public TrilhaMicroserviceUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Avaliacao> Avaliacoes => _context.Avaliacoes;
        public IQueryable<Trilha> Trilhas => _context.Trilhas;

        public void Add(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Add(avaliacao);
        }

        public void Add(Trilha avaliacao)
        {
            _context.Trilhas.Add(avaliacao);
        }

        public void Remove(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Remove(avaliacao);
        }

        public void Remove(Trilha avaliacao)
        {
            _context.Trilhas.Remove(avaliacao);
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
