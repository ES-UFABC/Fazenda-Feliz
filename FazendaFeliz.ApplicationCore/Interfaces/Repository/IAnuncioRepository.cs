using FazendaFeliz.ApplicationCore.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Interfaces.Repository
{
    public interface IAnuncioRepository : IRepository<Anuncio>
    {
        Task<List<AnuncioCompleto>> ObterTodosCompletos(int id_usuario, string tipo, string s);

        Task<List<Anuncio>> ObterAnunciosProdutor(int id_usuario);
    }
}
