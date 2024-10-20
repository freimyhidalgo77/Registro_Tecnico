using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models
{
	public class Clientes
	{

		[Key]

		public int ClienteId { get; set; }

		[Required(ErrorMessage = "Campo nombre obligatorio")]
		[RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
		public string? NombreCliente { get; set; }


		[Required(ErrorMessage = "Campo numero de whatsapp obligatorio")]
		public string? NumeroWhatsapp { get; set; }

	  
	}
}
  