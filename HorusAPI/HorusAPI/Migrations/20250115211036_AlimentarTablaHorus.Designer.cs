﻿// <auto-generated />
using System;
using HorusAPI.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HorusAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250115211036_AlimentarTablaHorus")]
    partial class AlimentarTablaHorus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HorusAPI.Models.Horus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActual")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("HorusDB");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Detalle = "Nuevo a la venta",
                            FechaActual = new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7505),
                            FechaCreacion = new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7491),
                            ImagenUrl = "",
                            Name = "Iphone",
                            Tarifa = 500.0
                        },
                        new
                        {
                            Id = 2,
                            Detalle = "Nueva generacion",
                            FechaActual = new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508),
                            FechaCreacion = new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508),
                            ImagenUrl = "",
                            Name = "Samsung",
                            Tarifa = 300.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
