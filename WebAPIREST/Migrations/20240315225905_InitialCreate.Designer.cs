﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebAPIREST.infraestrutura;

#nullable disable

namespace WebAPIREST.Migrations
{
    [DbContext(typeof(ConnectionContext))]
    [Migration("20240315225905_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebAPIREST.Models.Pessoa", b =>
                {
                    b.Property<int>("id_pessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_pessoa"));

                    b.Property<bool>("ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("data_atualizacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("data_nascimento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("genero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_pessoa");

                    b.ToTable("pessoa");
                });

            modelBuilder.Entity("WebAPIREST.Models.Telefone", b =>
                {
                    b.Property<int>("id_telefone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_telefone"));

                    b.Property<int>("Pessoaid_pessoa")
                        .HasColumnType("integer");

                    b.Property<string>("numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id_telefone");

                    b.HasIndex("Pessoaid_pessoa");

                    b.ToTable("telefone");
                });

            modelBuilder.Entity("WebAPIREST.Models.Telefone", b =>
                {
                    b.HasOne("WebAPIREST.Models.Pessoa", "Pessoa")
                        .WithMany("Telefones")
                        .HasForeignKey("Pessoaid_pessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("WebAPIREST.Models.Pessoa", b =>
                {
                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}