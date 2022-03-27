using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add (T objeto);
        public void BeginTransaction();
        public void CommitTransaction();
        Task<int> ExecuteStrategy(Task<int> dbFunction);
        Task Adicionar(T objeto);
        Task Atualizar(T objeto);
        Task Remover(T objeto);
        Task<T> ObterPorId(int id);
        Task<List<T>> ObterTodos();
        Task SaveChanges();
    }
}
