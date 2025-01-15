using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorusAPI.Models
{
    public class Horus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }

        public string ImagenUrl { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActual { get; set; }
    }
}
