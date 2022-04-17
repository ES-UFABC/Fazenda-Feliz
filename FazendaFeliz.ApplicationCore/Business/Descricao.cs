using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace FazendaFeliz.ApplicationCore.Business
{
    public class Descricao
    {
        public Anuncio Anuncio { get; set; }
        public Usuario Usuario { get; set; }
        public Reclamacao Reclamacao { get; set; }
    }
}
