using Microsoft.EntityFrameworkCore;

namespace Avaliacoes.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Avaliacao> Avaliacoes { get; set; }
    }
}