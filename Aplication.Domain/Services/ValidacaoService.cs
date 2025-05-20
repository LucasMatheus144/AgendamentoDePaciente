using Aplication.Domain.Entities;
using Aplication.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Services
{
    public class ValidacaoService
    {
        private readonly IRepository _repositorio;

        public ValidacaoService(IRepository repositorio)
        {
            this._repositorio = repositorio;
        }   
        public bool Validar<AllModels>(AllModels obj, out List<MensagemErro> erro)
        {
            var valida = new List<ValidationResult>();

            var IsValid = Validator.TryValidateObject(obj, new ValidationContext(obj), valida, true);

            erro = new List<MensagemErro>();

            if( !IsValid )
            {
                erro = valida
                    .Select( e => new MensagemErro
                           (
                               e.MemberNames.First(),
                               TratarMsgErro(e.ErrorMessage)
                           )
                    ).ToList();
            }

            return IsValid;
        }

     

        public string TratarMsgErro(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return "Deu zika";

            else if (msg.Contains("nome is required")) return "Mensagem tratada";

            else if (msg.Contains("uq_nome")) return "Esse nome ja existe!";

            return msg;
        }
    }
}
