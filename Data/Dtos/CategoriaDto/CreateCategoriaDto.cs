using System.ComponentModel.DataAnnotations;
using AluraFlix.Models;

namespace AluraFlix.Data.Dtos.CategoriaDto
{
    public class CreateCategoriaDto
    {
        [Required(ErrorMessage = "è obrigatorio o campo titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "é obrigatorio o campo cor")]
        public string Cor { get; set; }

    }
}