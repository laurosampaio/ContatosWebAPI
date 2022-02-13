using AutoMapper;
using Contatos.Application.Dtos;
using Contatos.Domain;

namespace Contatos.API.Helpers
{
    public class ContatosProfile : Profile
    {
        public ContatosProfile()
        {
            CreateMap<Contato, ContatoDto>().ReverseMap();
        }
    }
}