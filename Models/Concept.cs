using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIProject.Models
{
    [Table("ConceitoTitulo")]
    public class ConceptTitle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "A área deve conter apenas letras e espaços.")]
        [StringLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres")]
        public string? Titulo { get; set; }

    }
}