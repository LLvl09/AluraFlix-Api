using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "è obrigatorio o campo titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "é obrigatorio o campo cor")]
        public string Cor { get; set; }

        public virtual List<Video> Videos { get; set; }
    }
}