using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Data.Dtos.VideoDto
{
    public class UpdateVideoDto
    {
        [Required(ErrorMessage = "Informe o titulo")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a url")]
        [StringLength(50, ErrorMessage = "o maximo é 50 caracteres")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Informe o campo categoriaId")]
        public int CategoriaId { get; set; }
    }
}