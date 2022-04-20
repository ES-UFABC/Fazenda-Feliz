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
    public class ReclamacaoRepository : Repository<Reclamacao>, IReclamacaoRepository
    {
        public ReclamacaoRepository(AppDbContext context) : base(context) { }

        public Task<List<Reclamacao>> ObterReclamacoesProdutor(int id_produtor)
        {
            var reclamacoesProdutor =
                from r in _Context.Reclamacoes
                join a in _Context.Anuncios
                on r.Id_Anuncio equals a.Id
                join produtor in _Context.Usuarios
                on a.Id_Usuario equals produtor.Id
                where a.Id_Usuario == id_produtor   
                select r;


            return reclamacoesProdutor.AsNoTracking().ToListAsync();
        }

    }
}
