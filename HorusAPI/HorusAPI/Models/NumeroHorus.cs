using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorusAPI.Models
{
    public class NumeroHorus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HorusNo { get; set; }
        [Required]
        public int HorusId { get; set; }
        [ForeignKey("HorusId")]
        public Horus Horus { get; set; }

        public string DetalleEspecial { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActual { get; set; }
    }
}
