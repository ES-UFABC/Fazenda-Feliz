#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FazendaFeliz.Web.Models;

namespace FazendaFeliz.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FazendaFeliz.Web.Models.Perfil> Perfil { get; set; }
    }
}
