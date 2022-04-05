namespace FazendaFeliz.ApplicationCore.Business
{
    public class Anuncio : Entity
    {
        public string Titulo { get; set; }
        public string Localizacao { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public string Imagem_Base64 { get; set; }
        public Boolean Oculto { get; set; }
        public Boolean Favorito { get; set; }

    }
}
