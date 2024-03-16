﻿using Microsoft.EntityFrameworkCore;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql(
                "Server=localhost;"
                    + "Port=5432;"
                    + "Database=postgresDB;"
                    + "User Id=postgres;"
                    + "Password=root;"
            );
    }
}