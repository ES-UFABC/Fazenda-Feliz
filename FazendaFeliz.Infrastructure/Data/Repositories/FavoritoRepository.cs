using Microsoft.EntityFrameworkCore;
using FazendaFeliz.ApplicationCore.Business;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.Infrastructure.Data.Repositories
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {
        public FavoritoRepository(AppDbContext context) : base(context) { }

        public async Task<List<Anuncio>> ObterAnunciosFavoritosPorUsuario(int id_usuario)
        {
            var anunciosFavoritos =
                from f in _Context.Favoritos
                join a in _Context.Anuncios
                    on f.Id_Anuncio equals a.Id
                where f.Id_Usuario == id_usuario
                select a;

            return await anunciosFavoritos.AsNoTracking().ToListAsync();
        }

        public async Task<Favorito> ObterPorUsuarioAnuncio(int id_usuario, int id_anuncio)
        {
            return await _Context.Favoritos.Where(f => f.Id_Anuncio == id_anuncio && f.Id_Usuario == id_usuario).FirstOrDefaultAsync();
        }
    }
}
