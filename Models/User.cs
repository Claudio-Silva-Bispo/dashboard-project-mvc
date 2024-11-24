using System.ComponentModel.DataAnnotations;
namespace BIProject.Models;

public class User
{
    [Key]    
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "A área deve conter apenas letras e espaços.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@ezzeseguros\.com.br$", ErrorMessage = "O domínio do e-mail deve ser '@ezzeseguros.com.br'")]
    public string? Email { get; set; }

    [Required]
    // Propriedade Perfil com valor padrão como "Comum" para todos os cadastros
    public string? Perfil { get; set; } = "Comum";

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=(.*\d){6})(?=(.*[a-z]){1})(?=(.*[A-Z]){1})(?=(.*[\W_]){1}).{8,}$", ErrorMessage = "A senha deve conter exatamente 6 números, 2 letras (uma maiúscula e uma minúscula), e ao menos 1 símbolo.")]
    public string? Senha { get; set; }

    [Required]
    public string? Area { get; set;}
}
