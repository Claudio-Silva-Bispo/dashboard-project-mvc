using System.ComponentModel.DataAnnotations;

namespace BIProject.Models
{
    public class LoginHistory
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }


        [Required]
        public DateTime DataHora { get; set; }

        // Relacionar com o usuário pelo IdUsuario
        public User? Usuario { get; set; }
    }
}
