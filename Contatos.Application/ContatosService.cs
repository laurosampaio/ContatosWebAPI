using AutoMapper;
using Contatos.Application.Contratos;
using Contatos.Application.Dtos;
using Contatos.Domain;
using Contatos.Persistence.Contratos;
using System;
using System.Threading.Tasks;

namespace Contatos.Application
{
    public class ContatosService : IContatoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IContatoPersist _contatoPersist;
        private readonly IMapper _mapper;
        public ContatosService(IGeralPersist geralPersist,
                             IContatoPersist contatoPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _contatoPersist = contatoPersist;
            _mapper = mapper;
        }
        public async Task<ContatoDto> AddContatos(ContatoDto model)
        {
            try
            {
                var contato = _mapper.Map<Contato>(model);

                _geralPersist.Add<Contato>(contato);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var contatoRetorno = await _contatoPersist.GetContatoByIdAsync(contato.Id);

                    return _mapper.Map<ContatoDto>(contatoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContatoDto> UpdateContato(int contatoId, ContatoDto model)
        {
            try
            {
                var contato = await _contatoPersist.GetContatoByIdAsync(contatoId);
                if (contato == null) return null;

                model.Id = contato.Id;

                _mapper.Map(model, contato);

                _geralPersist.Update<Contato>(contato);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var contatoRetorno = await _contatoPersist.GetContatoByIdAsync(contato.Id);

                    return _mapper.Map<ContatoDto>(contatoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteContato(int contatoId)
        {
            try
            {
                var contato = await _contatoPersist.GetContatoByIdAsync(contatoId);
                if (contato == null) throw new Exception("Contato não encontrado.");

                _geralPersist.Delete<Contato>(contato);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContatoDto[]> GetAllContatosAsync()
        {
            try
            {
                var contatos = await _contatoPersist.GetAllContatosAsync();
                if (contatos == null) return null;

                var resultado = _mapper.Map<ContatoDto[]>(contatos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContatoDto> GetContatoByIdAsync(int contatoId)
        {
            try
            {
                var contato = await _contatoPersist.GetContatoByIdAsync(contatoId);
                if (contato == null) return null;

                var resultado = _mapper.Map<ContatoDto>(contato);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }              
    }
}