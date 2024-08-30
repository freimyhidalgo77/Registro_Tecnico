using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
	public class Tecnicos
	{
		[Key]
		//Atributos Tecnico (Id, Nombre, Sueldo por hora)
		public int TecnicoId { get; set; }

		[Required(ErrorMessage = "El campo nombre es obligatorio.")]

		[RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
		public string NombreTecnico { get; set; }


		[Required (ErrorMessage = "Campo sueldo por hora obligatrio ")]   

		public decimal SueldoHora {get; set;}    
		     
	}   
}  
   