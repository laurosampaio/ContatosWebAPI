using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contatos.Domain;
using Contatos.Persistence.Contextos;
using Contatos.Persistence.Contratos;

namespace Contatos.Persistence
{
    public class ContatoPersist : IContatoPersist
    {
        private readonly ContatosContext _context;
        public ContatoPersist(ContatosContext context)
        {
            _context = context;            
        }

        public async Task<Contato[]> GetAllContatosAsync()
        {
            IQueryable<Contato> query = _context.Contatos;

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Contato> GetContatoByIdAsync(int id)
        {
            IQueryable<Contato> query = _context.Contatos;

         
            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}