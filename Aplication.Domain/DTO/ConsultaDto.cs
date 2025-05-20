using Aplication.Domain.ValuesObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.DTO
{
    public class ConsultaDto
    {
        public int Id { get; set; }
        public DateTime PrevisaoConsulta { get; set; }
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public SituacaoConsulta SituacaoConsulta { get; set; }

        public int IdPaciente { get; set; }
    }
}
