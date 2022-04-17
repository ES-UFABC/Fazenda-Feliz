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

        public Task<List<AnuncioCompleto>> ObterTodosCompletos(int id_usuario, string tipo, string s)
        {
            var anunciosFavoritos =
                from a in _Context.Anuncios
                join u in _Context.Usuarios
                    on a.Id_Usuario equals u.Id
                where 
                    (tipo != "produto" || a.Categoria.ToLower().Contains(s)) &&
                    (tipo != "anuncios" || a.Titulo.ToLower().Contains(s)) &&
                    (tipo != "produtores" || u.Nome.ToLower().Contains(s)) &&
                    (tipo != "localizações" || a.Localizacao.ToLower().Contains(s))
                let blnFavorito = _Context.Favoritos.Where(f => f.Id_Anuncio == a.Id && f.Id_Usuario == id_usuario).Any()
                select new AnuncioCompleto()
                {
                    Anuncio = a,
                    Produtor = u,
                    Favorito = blnFavorito
                };

            return anunciosFavoritos.AsNoTracking().ToListAsync();
        }

        public Task<List<Anuncio>> ObterAnunciosProdutor(int id_usuario)
        {
            var anunciosProdutor =
                from a in _Context.Anuncios
                where a.Id_Usuario == id_usuario
                select a;
            return anunciosProdutor.AsNoTracking().ToListAsync();
        }

    }
}
