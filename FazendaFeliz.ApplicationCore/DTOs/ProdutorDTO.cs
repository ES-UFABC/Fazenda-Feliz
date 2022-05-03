using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazendaFeliz.ApplicationCore.Business
{
    public class ProdutorDTO
    {
        public Usuario Usuario { get; set; }

        public List<Reclamacao> Reclamacaos { get; set; }
    }
}
