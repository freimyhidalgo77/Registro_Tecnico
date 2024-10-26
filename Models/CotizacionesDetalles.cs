using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class CotizacionesDetalles
    {

        [Key]
        public int DetalleId { get; set; }

        [ForeignKey("CotizacionId")]
        public int CotizacionesId { get; set; }
        public Cotizaciones? cotizaciones { get; set; }


        [ForeignKey("ArticuloId")]
        public int ArticuloId { get; set; }
        public Articulos? Articulos { get; set; }


        [Required(ErrorMessage = "obligatorio introducir una cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "obligatorio introducir un precio")]
        public decimal precio { get; set; }


    }
}
