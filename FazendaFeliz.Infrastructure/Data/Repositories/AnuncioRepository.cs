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
    public class AnuncioRepository : Repository<Anuncio>, IAnuncioRepository
    {
        public AnuncioRepository(AppDbContext context) : base(context) { }

        public Task<List<AnuncioComFavorito>> ObterTodosComFavorito(int id_usuario)
        {
            var anunciosFavoritos =
                from a in _Context.Anuncios
                let blnFavorito = _Context.Favoritos.Where(f => f.Id_Anuncio == a.Id && f.Id_Usuario == id_usuario).Any()
                select new AnuncioComFavorito()
                {
                    Id = a.Id,
                    Id_Usuario = a.Id_Usuario,
                    Titulo = a.Titulo,
                    Localizacao = a.Localizacao,
                    Categoria = a.Categoria,
                    Preco = a.Preco,
                    Descricao = a.Descricao,
                    Imagem_Base64 = a.Imagem_Base64,
                    Favorito = blnFavorito
                };

            return anunciosFavoritos.AsNoTracking().ToListAsync();
        }
    }
}
