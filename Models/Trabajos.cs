using RegistroTecnicos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RegistroTecnicos.Models
{
	public class Trabajos  
	{

		[Key]
	    public int TrabajoId { get; set; }

		[Required(ErrorMessage = "Favor ingresar una fecha")]
		public DateTime Fecha { get; set; }

		[ForeignKey("ClienteId")]
		[Required(ErrorMessage = "Favor seleccionar un id para el cliente")]

		public int ClienteId { get; set; }


		[ForeignKey("TecnicoId")]
		[Required(ErrorMessage = "Favor ingresar seleccionar un id para tecnico")]

		public int TecnicoId { get; set; }

		[ForeignKey("NombreCliente")]
		[Required(ErrorMessage = "Favor ingresar el nombre del cliente")]

		public string NombreCliente { get; set; }

		[ForeignKey("NombreTecnico")]
		[Required(ErrorMessage = "Favor seleccionar el nombre del tecnico")]

		public string NombreTecnico { get; set; }

		[ForeignKey("Descripcion")]
		[Required(ErrorMessage = "Favor agregar una descripcion")]

		public string Descripcion { get; set; }

		[Required(ErrorMessage = "Favor llenar el campo monto")]
		public decimal Monto { get; set; }


		public Clientes Clientes { get; set; }

		public Tecnicos Tecnicos { get; set; }




	}
}
