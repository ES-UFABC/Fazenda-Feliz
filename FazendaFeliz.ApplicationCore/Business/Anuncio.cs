using System.ComponentModel.DataAnnotations.Schema;

namespace FazendaFeliz.ApplicationCore.Business
{
    public class Anuncio : Entity
    {
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public string Titulo { get; set; }
        public string Localizacao { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Imagem_Base64 { get; set; }
    }

    public class AnuncioCompleto
    {
        public Anuncio Anuncio { get; set; }
        public Usuario Produtor { get; set; }
        public Boolean Favorito { get; set; }
    }
}
