﻿// <auto-generated />
using Demostrativo.Jwt.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demostrativo.Jwt.Persistence.Migrations
{
    [DbContext(typeof(PersistenceDbContext))]
    partial class PersistenceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Demostrativo.Jwt.Domain.Ejemplo.EjemploEntity", b =>
                {
                    b.Property<int>("EjemploId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EjemploId"));

                    b.Property<string>("NombreEjemplo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("EjemploId");

                    b.ToTable("Ejemplo");

                    b.HasData(
                        new
                        {
                            EjemploId = 1,
                            NombreEjemplo = "ejemplo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
