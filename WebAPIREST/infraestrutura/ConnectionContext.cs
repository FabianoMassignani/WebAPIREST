using Microsoft.EntityFrameworkCore;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432;" +
            "Database=postgresDB;" +
            "User Id=root;" +
            "Password=root;");
    }
}
