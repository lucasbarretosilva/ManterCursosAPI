namespace ManterCursosAPI.Models
{
    public class Administrador
    {
        public int AdministradorId { get; set; }

        public string NomeAdm { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
    }
}
