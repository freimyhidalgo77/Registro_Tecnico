using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RegistroTecnicos.Models
{
    public class Prioridades
    {

        [Key]

        public int PrioridadId { get; set; }

        public string Descripcion { get; set; }

        public int Tiempo { get; set; }


    }
}
           