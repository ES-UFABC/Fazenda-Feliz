using FazendaFeliz.ApplicationCore.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Interfaces.Repository
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        Task<List<Anuncio>> ObterAnunciosFavoritosPorUsuario(int id_usuario);
        Task<Favorito> ObterPorUsuarioAnuncio(int id_usuario, int id_anuncio);
        Task<Favorito> ObterPorUsuarioProdutor(int id_usuario, int id_anuncio);
        Task<List<Usuario>> ObterProdutoresFavoritosPorUsuario(int id_usuario);
    }
}
