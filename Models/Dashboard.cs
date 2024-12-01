using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIProject.Models
{
    [Table("Dashboard")]
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? NomeRelatorio {get; set; }

        [Required]
        public string? Solicitante {get; set; }

        [Required]
        public string? AnalistaCriador {get; set; }

        [Required(ErrorMessage = "O link é obrigatório")]
        public string? Link { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

    }
}