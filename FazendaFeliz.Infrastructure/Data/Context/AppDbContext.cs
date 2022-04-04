using Microsoft.EntityFrameworkCore;
using FazendaFeliz.ApplicationCore.Business;

namespace FazendaFeliz.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Reclamacao> Reclamacaos { get; set; }
    }
}
