using Microsoft.EntityFrameworkCore;

namespace ToDo.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Trilha> Trilhas { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}