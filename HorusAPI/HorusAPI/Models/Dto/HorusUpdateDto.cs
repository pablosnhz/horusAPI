using System.ComponentModel.DataAnnotations;

namespace HorusAPI.Models.Dto
{
    public class HorusUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public DateTime DateCreacion { get; set; }
        public DateTime FechaActual { get; set; }

    }
}
