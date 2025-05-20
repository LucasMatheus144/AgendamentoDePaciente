using Aplication.Domain.DTO;
using Aplication.Domain.Entities;
using Aplication.Domain.Repository;
using Aplication.Domain.ValuesObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Services
{
    public class ConsultaService
    {
        private readonly IRepository _repositorio;
        private readonly ValidacaoService validacao;
        private readonly PacienteService servico;

        public ConsultaService(IRepository repositorio, ValidacaoService validacao, PacienteService service)
        {
            this._repositorio = repositorio;
            this.validacao = validacao;
            this.servico = service;
        }

        public bool Cadastra(ConsultaDto obj, out List<MensagemErro> erros)
        {

            var paciente = servico.ListarPorId(obj.IdPaciente);

            var novo = new Consulta
            {
                PrevisaoConsulta = obj.PrevisaoConsulta,
                SituacaoConsulta = SituacaoConsulta.Agendada,
                Paciente = paciente
            };

            var valida = validacao.Validar(novo, out erros);
            if (valida && ValidaDisponibilidade(novo))
            {
                try
                {
                    _repositorio.Incluir(novo);
                    return true;
                }
                catch (Exception ex)
                {
                    erros.Add(new MensagemErro("Cadastro Consulta", validacao.TratarMsgErro(ex.InnerException?.ToString())));
                }
            }
            return false;
        }

        public bool Exclusao(int id)
        {
            var consulta = _repositorio.ConsultaPorId<Consulta>(id);
            if (consulta != null)
            {
                try
                {
                    _repositorio.Excluir(consulta);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(validacao.TratarMsgErro(ex.InnerException?.ToString()));
                }
            }
            return false;
        }

        public List<Consulta> AllConsultas()
        {
            return _repositorio.Consultar<Consulta>();
        }

        public Consulta ConsultaPorId(int id)
        {
            return _repositorio.ConsultaPorId<Consulta>(id);
        }

        public List<Consulta> ConsultaDiaria(DateTime date)
        {
            return _repositorio.Consultar<Consulta>()
                 .Where(x => x.PrevisaoConsulta.Date == date.Date)
                 .ToList(); 
        }

        public bool ValidaDisponibilidade(Consulta obj)
        {
            var consulta = _repositorio.Consultar<Consulta>()
                .Where(x => x.PrevisaoConsulta == obj.PrevisaoConsulta)
                .Count();
            if (consulta >= 4) // So pode haber 3 agendamentos para o dia
            {
                var erro = new MensagemErro("Consulta", "É possivel 3 agendamentos para o mesmo dia");
                return false;
            }
            return true;
        }
               
    }
}
