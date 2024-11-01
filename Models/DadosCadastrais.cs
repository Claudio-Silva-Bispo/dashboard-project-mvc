using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DelfosMachine.Models
{
    public class DadosCadastrais
    {
        [Key]
        public int Id { get; set; }

        // Dados do Cliente
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public required Cliente Cliente { get; set; }

        // Preferências do Cliente
        public int IdPreferenciaCliente { get; set; }
        [ForeignKey("IdPreferenciaCliente")]
        public required PreferenciaCliente PreferenciaCliente { get; set; }

        // Rotina de Cuidado do Cliente
        public int IdRotinaCuidadoCliente { get; set; }
        [ForeignKey("IdRotinaCuidadoCliente")]
        public required RotinaCuidadoCliente RotinaCuidadoCliente { get; set; }
    }
}
