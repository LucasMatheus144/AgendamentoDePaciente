using Aplication.Domain.Interface;
using Aplication.Domain.ValuesObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Entities
{
    public class Consulta : IEntidade
    {
        public int Id { get; set; }
        public DateTime PrevisaoConsulta { get; set; }
        public DateTime DataCadastro {  get; private set; } = DateTime.Now;
        public SituacaoConsulta SituacaoConsulta { get; set; }
        public Paciente Paciente { get; set; }
    }
}
