using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Models
{
    public class TrabajosDetalle
    {

            [Key]

            public int DetalleId { get; set; }

            [ForeignKey("TrabajoId")]
            public int TrabajoId { get; set; }
            public Trabajos? Trabajos { get; set; }

            [ForeignKey("ArticuloId")]
            public int ArticuloId { get; set; }
            public Articulos? Articulo { get; set; }

            [Required(ErrorMessage = "Ingrese una cantidad")]
            public int cantidad { get; set; }

            [Required(ErrorMessage = "Ingrese un precio")]
            public decimal Precio { get; set; }

            [Required(ErrorMessage = "Ingrese un costo")]
            public decimal Costo { get; set; }


        }
}
