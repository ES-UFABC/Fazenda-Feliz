using FazendaFeliz.ApplicationCore.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Interfaces.Repository
{
    public interface IAnuncioRepository : IRepository<Anuncio>
    {
        Task<List<AnuncioComFavorito>> ObterTodosComFavorito(int id_usuario);

        Task<List<Anuncio>> ObterAnunciosProdutor(int id_usuario);
    }
}
