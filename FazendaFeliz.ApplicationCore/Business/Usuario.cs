namespace FazendaFeliz.ApplicationCore.Business
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public string Foto { get; set; }
    }
}
