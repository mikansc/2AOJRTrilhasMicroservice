using Microsoft.EntityFrameworkCore;

namespace Trilhas.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Trilha> Trilhas { get; set; }
    }
}