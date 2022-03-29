using FazendaFeliz.ApplicationCore.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void AdicionarCasoInexistente(Usuario usuario);
        Task<Usuario> ObterUsuarioOuInserir(string email, string nome);
        Usuario ObterPorEmail(string email);
        Task<int> ObterIdOuInserir(string email, string nome);
    }
}
