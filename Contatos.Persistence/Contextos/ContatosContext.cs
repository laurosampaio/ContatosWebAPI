using Microsoft.EntityFrameworkCore;
using Contatos.Domain;

namespace Contatos.Persistence.Contextos
{
    public class ContatosContext : DbContext
    {
        public ContatosContext(DbContextOptions<ContatosContext> options) 
            : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}