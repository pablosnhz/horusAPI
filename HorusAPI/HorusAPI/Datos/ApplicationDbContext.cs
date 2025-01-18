using HorusAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HorusAPI.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Horus> HorusDB { get; set; }
        public DbSet<NumeroHorus> NumeroHorusDB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Horus>().HasData(
                    new Horus()
                    {
                        Id = 1,
                        Name = "Iphone",
                        Detalle = "Nuevo a la venta",
                        ImagenUrl = "",
                        Tarifa = 500,
                        FechaCreacion = DateTime.Now,
                        FechaActual = DateTime.Now
                    },
                    new Horus()
                    {
                        Id = 2,
                        Name = "Samsung",
                        Detalle = "Nueva generacion",
                        ImagenUrl = "",
                        Tarifa = 300,
                        FechaCreacion = DateTime.Now,
                        FechaActual = DateTime.Now
                    }
                );
        }
    }
}
