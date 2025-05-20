using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Entities
{
    public class MensagemErro
    {
        public string Propriedade { get; set; }

        public string Menssagem { get; set; }

        public MensagemErro (string Propriedade, string Mensaggem)
        {
            this.Propriedade = Propriedade;
            this.Menssagem = Mensaggem;
        }

    }
}
