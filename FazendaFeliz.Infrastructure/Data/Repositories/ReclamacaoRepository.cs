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

        public async Task<List<Reclamacao>> ObterReclamacoesPorIdAnuncio(int idAnuncio)
        {
            return await _Context.Reclamacoes.Where(t=> t.Id_Anuncio == idAnuncio).ToListAsync();
        }

    }
}
