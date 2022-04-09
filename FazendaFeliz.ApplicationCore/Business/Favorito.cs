using System.ComponentModel.DataAnnotations.Schema;

namespace FazendaFeliz.ApplicationCore.Business
{
    public class Favorito : Entity
    {
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        [ForeignKey("Anuncio")]
        public int Id_Anuncio { get; set; }
    }
}
