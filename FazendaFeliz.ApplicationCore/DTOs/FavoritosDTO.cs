using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Business
{
    public class FavoritosDTO
    {
        public List<Usuario> Usuarios { get; set; }

        public List<Anuncio> Anuncios { get; set;}
    }
}
