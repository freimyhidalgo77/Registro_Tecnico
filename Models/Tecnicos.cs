using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
	public class Tecnicos
	{
		[Key]
		//Atributos Tecnico (Id, Nombre, Sueldo por hora)
		public int TecnicoId { get; set; }

		[Required (ErrorMessage = "Por favor, ingrese el nombre del Tecnico")]
		public string NombreTecnico { get; set; }

		[Required (ErrorMessage = "Por favor ingrese el valor del sueldo por hora ")]   

		public float SueldoHora {get; set;}   
		    
		   
	}   
} 
  