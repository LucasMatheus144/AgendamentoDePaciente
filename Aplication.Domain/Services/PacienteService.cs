using Aplication.Domain.Entities;
using Aplication.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Services
{
    public class PacienteService
    {
        private readonly IRepository _repositorio;
        private readonly ValidacaoService validacao;

        public PacienteService(IRepository repositorio, ValidacaoService validacao)
        {
            this._repositorio = repositorio;
            this.validacao = validacao;
        }


        public bool Cadastra(Paciente obj, out List<MensagemErro> erros)
        {
            var valida = validacao.Validar(obj, out erros);

            if (valida)
            {
                try
                {
                    _repositorio.Incluir(obj);
                    return true;
                }
                catch (Exception ex)
                {
                    erros.Add(new MensagemErro("Cadastro Paciente", validacao.TratarMsgErro(ex.InnerException?.ToString())));
                }
            }

            return false;
        }

        public List<Paciente> AllPaciente()
        {
            return _repositorio.Consultar<Paciente>();
        }

        public Paciente ListarPorId(int id)
        {
            return _repositorio.ConsultaPorId<Paciente>(id);
        }

        public Paciente Editar(Paciente obj)
        {
            var procura = ListarPorId(obj.Id);

            if (procura != null)
            {
                procura.Nome = obj.Nome;
                procura.DataNascimento = obj.DataNascimento;
                procura.Observacao = obj.Observacao;
                procura.SituacaoPaciente = obj.SituacaoPaciente;
                procura.SituacaoGrupoRisco = obj.SituacaoGrupoRisco;

                _repositorio.Salvar(obj);
                return procura;
            }
            return null;
        }

        public bool Excluir(int id)
        {
            var procura = ListarPorId(id);

            if (procura != null)
            {
                _repositorio.Excluir(procura);
                return true;
            }

            return false;
        }

       
    }
}
