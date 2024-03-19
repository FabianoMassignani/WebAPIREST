using Microsoft.EntityFrameworkCore;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Telefone>()
                .HasOne(p => p.Pessoa)
                .WithMany(p => p.Telefones)
                .HasForeignKey(p => p.PessoaId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var server = config["DBServer"];
            var port = config["DBPort"];
            var database = config["DBName"];
            var username = config["DBUsername"];
            var password = config["DBPassword"];

            optionsBuilder.UseNpgsql(
                $"Server={server};Port={port};Database={database};User Id={username};Password={password}"
            );
        }
    }
}
