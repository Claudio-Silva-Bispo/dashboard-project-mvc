using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIProject.Models
{
    [Table("ConceitoDetalhes")]
    public class ConceptDetails
    {
        [Key]
        public int Id { get; set; }

        public int IdTitulo { get; set; }

         [Required(ErrorMessage = "O título é obrigatório")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O subtítulo é obrigatório")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "A área deve conter apenas letras e espaços.")]
        [StringLength(50, ErrorMessage = "O sub-título deve ter no máximo 50 caracteres")]
        public string? Subtitulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string? Descricao { get; set; }

       
    }
}