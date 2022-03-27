namespace FazendaFeliz.Web.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Descricao { get; set; }
        public bool Produtor { get; set; }
        public Perfil()
        {

        }

    }
}
