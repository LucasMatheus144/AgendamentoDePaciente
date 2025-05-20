using Aplication.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Repository
{
    public interface IRepository
    {
        void Incluir(object model);

        void Salvar(object model);

        void Excluir(object model);

        T ConsultaPorId<T>(int id) where T : IEntidade;

        List<X> Consultar<X>();
    }
}
