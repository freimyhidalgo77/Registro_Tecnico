using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class CotizacionesDetalles
    {

        [Key]
        public int DetalleId { get; set; }

        [ForeignKey("Cotizaciones")]
        public int CotizacionId { get; set; }
        public Cotizaciones? Cotizaciones { get; set; }


        [ForeignKey("Articulo")]
        public int ArticuloId { get; set; }
        public Articulos? Articulo { get; set; }


        [Required(ErrorMessage = "Obligatorio introducir una cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Obligatorio introducir un precio")]
        public decimal precio { get; set; }


    }
}
