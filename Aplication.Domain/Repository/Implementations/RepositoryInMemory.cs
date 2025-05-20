using Aplication.Domain.Interface;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Domain.Repository.Implementations
{
    public class RepositoryInMemory : IRepository
    {
        protected readonly ISessionFactory session;

        public RepositoryInMemory(ISessionFactory sessionFactory)
        {
            session = sessionFactory;
        }

        public List<T> Consultar<T>()

        {
            using var abrir = session.OpenSession();
            return abrir.Query<T>().ToList();
        }

        public T ConsultaPorId<T>(int id) where T : IEntidade

        {
            using var abrir = session.OpenSession();
            return abrir.Get<T>(id);
        }

        public void Incluir(object model)
        {
            using var abrir = session.OpenSession();
            using var transaction = abrir.BeginTransaction();

            abrir.Save(model);
            transaction.Commit();
        }
        public void Excluir(object model)
        {
            using var abrir = session.OpenSession();
            using var transaction = abrir.BeginTransaction();

            abrir.Delete(model);
            transaction.Commit();
        }

        public void Salvar(object model)
        {
            using var abrir = session.OpenSession();
            using var transaction = abrir.BeginTransaction();

            abrir.Update(model);
            transaction.Commit();
        }
    }
}
