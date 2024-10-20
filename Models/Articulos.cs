using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Articulos
    {
        [Key]

        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "Favor llenar el campo descripcion")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Favor llenar el campo costo")]
        public decimal costo { get; set; }

        [Required(ErrorMessage = "Favor llenar el campo precio")]
        public decimal precio { get; set; }

        [Required(ErrorMessage = "Favor llenar el campo existecia")]
        public int existencia { get; set; } 

    }
}       
  