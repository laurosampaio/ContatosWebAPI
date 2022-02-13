using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contatos.Application.Dtos
{
    public class ContatoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Empresa { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "O campo {0} está com número inválido")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "É necessário ser um {0} válido")]
        public string Email { get; set; }

        public string ImagemURL { get; set; }

    }
}