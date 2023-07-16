using Microsoft.EntityFrameworkCore;

namespace Notificacoes.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Notificacao> Notificacoes { get; set; }
    }
}