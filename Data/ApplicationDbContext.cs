using BIProject.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public required DbSet<User> Usuario { get; set; }

    public required DbSet<LoginHistory> Login { get; set; }

    

}
