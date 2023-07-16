using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trilhas.Models;

namespace TrilhasMicroservice.Repositories
{
    public interface IUnitOfWork
    {
        public IQueryable<Trilha> Trilhas { get; }
        void Add(Trilha avaliacao);
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

        public IQueryable<Trilha> Trilhas => _context.Trilhas;

        public void Add(Trilha avaliacao)
        {
            _context.Trilhas.Add(avaliacao);
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
