using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Cotizaciones
    {

        [Key]

        public int CotizacionId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;


        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public Clientes? Clientes { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una observacion")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]

        public string? Observacion { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un monto")]

        public decimal Monto { get; set; }

        [ForeignKey("CotizacionId")]
        public ICollection<CotizacionesDetalles> CotizacionesDetalles { get; set; } = new List<CotizacionesDetalles>();



    }
}
