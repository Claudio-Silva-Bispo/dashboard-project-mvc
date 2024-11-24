using System.ComponentModel.DataAnnotations;

namespace BIProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Informe um Email válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }


    }
}
