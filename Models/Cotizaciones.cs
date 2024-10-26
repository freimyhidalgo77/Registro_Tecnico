using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Cotizaciones
    {

        [Key]

        public int CotizacionId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una fecha")]
        public DateTime Fecha { get; set; }


        [ForeignKey("Clientes")]
        public int ClienteId { get; set; }
        public Clientes? Clientes { get; set; }


        [Required(ErrorMessage = "Es obligatorio introducir una observacion")]
        public string? Observacion { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un monto")]

        public decimal Monto { get; set; }

        public ICollection<CotizacionesDetalles> CotizacionesDetalles { get; set; } = new List<CotizacionesDetalles>();



    }
}
