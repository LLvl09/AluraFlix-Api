using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Data.Dtos.CategoriaDto
{
    public class ReadCategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "è obrigatorio o campo titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "é obrigatorio o campo cor")]
        public string Cor { get; set; }

    }
}