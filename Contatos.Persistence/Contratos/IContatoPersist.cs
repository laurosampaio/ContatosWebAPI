using System.Threading.Tasks;
using Contatos.Domain;

namespace Contatos.Persistence.Contratos
{
    public interface IContatoPersist
    {
        Task<Contato[]> GetAllContatosAsync();
        Task<Contato> GetContatoByIdAsync(int id);
    }
}