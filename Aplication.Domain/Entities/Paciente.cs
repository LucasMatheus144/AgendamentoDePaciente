using Aplication.Domain.Interface;
using Aplication.Domain.ValuesObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Entities
{
    public class Paciente : IEntidade
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Observacao { get; set; }

        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public DateTime? DataNascimento { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int Idade
        {
            get
            {
                if (!DataNascimento.HasValue) return 0;

                var anoAtual = DateTime.Now.Year;
                var idade = anoAtual - DataNascimento.Value.Year;

                var aniversario = DataNascimento.Value.AddYears(idade);
                if (aniversario > DateTime.Today) idade--;

                return idade;
            }
        }




        public SituacaoPaciente SituacaoPaciente { get; set; }

        public SituacaoGrupoRisco SituacaoGrupoRisco { get; set; }
    }
}
