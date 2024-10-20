using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RegistroTecnicos.Models
{
    public class Prioridades
    {

        [Key]

        public int PrioridadId { get; set; }

        [Required(ErrorMessage = "Campo descripcion obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo letras")]
        public string? descripcion { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public int Tiempo { get; set; }


    }
}
                  