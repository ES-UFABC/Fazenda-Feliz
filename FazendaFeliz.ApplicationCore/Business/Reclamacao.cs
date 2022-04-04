namespace FazendaFeliz.ApplicationCore.Business
{
    public class Reclamacao : Entity
    {
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem_Base64 { get; set; }
    }
}
