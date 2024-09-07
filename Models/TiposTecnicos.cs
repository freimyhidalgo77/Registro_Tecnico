using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{

    public class TiposTecnicos
    {
        [Key]
        public int TipoId { get; set; }

        [Required(ErrorMessage = "Favor de agregar una descripcion")]

        public string Descripcion { get; set; }




    }
}
            