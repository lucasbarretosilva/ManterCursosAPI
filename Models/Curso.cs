using System.ComponentModel.DataAnnotations;

namespace ManterCursosAPI.Models
{
    public class Curso
    {
        public int CursoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataTermino { get; set; }

        public int QtdAlunos { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

    }
}
