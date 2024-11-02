

using DelfosMachine.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PreferenciaCliente> PreferenciasClientes { get; set; }
    public DbSet<EnderecoPreferencia> EnderecoPreferencia { get; set; }
    public DbSet<HorarioPreferencia> PreferenciaHorario { get; set; }
    public DbSet<TurnoPreferencia> Turno { get; set; }
    public DbSet<DiaSemanaPreferencia> PreferenciaDia  { get; set; }
    public DbSet<RotinaCuidadoCliente> RotinaCuidado  { get; set; }
    public DbSet<DadosCadastrais> DadosCadastrais  { get; set; }
}
