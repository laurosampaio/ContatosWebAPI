using System.Threading.Tasks;
using Contatos.Application.Dtos;

namespace Contatos.Application.Contratos
{
    public interface IContatoService
    {
        Task<ContatoDto> AddContatos(ContatoDto model);
        Task<ContatoDto> UpdateContato(int contatoId, ContatoDto model);
        Task<bool> DeleteContato(int contatoId);

        Task<ContatoDto[]> GetAllContatosAsync();
        Task<ContatoDto> GetContatoByIdAsync(int contatoId);
    }
}