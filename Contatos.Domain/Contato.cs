using System;

namespace Contatos.Domain
{
    public class Contato
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Empresa { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string ImagemURL { get; set; }

        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}