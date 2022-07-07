namespace ManterCursosAPI.Models
{
    public class Log
    {
        public int LogId { get; set; }

        public string Alteracao { get; set; } = string.Empty;

        public DateTime DataInclusao { get; set; }

        public DateTime? DataUltAlteracao { get; set; }

        public int CursoId { get; set; }

        public int AdministradorId { get; set; }

       

        public Administrador? Administrador { get; set; }

        public Curso? Curso { get; set; }


    }
}
