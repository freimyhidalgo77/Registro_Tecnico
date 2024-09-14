using RegistroTecnicos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Trabajos
{
	[Key]
	public int TrabajoId { get; set; }

	[Required(ErrorMessage = "Favor ingresar una fecha")]
	public DateTime Fecha { get; set; }

	[ForeignKey("Clientes")]
	[Required(ErrorMessage = "Favor seleccionar un id para el cliente")]
	public int ClienteId { get; set; }

	[ForeignKey("Tecnicos")]
	[Required(ErrorMessage = "Favor seleccionar un id para el técnico")]
	public int TecnicoId { get; set; }

	[ForeignKey("TiposTecnicos")]
	[Required(ErrorMessage = "Favor seleccionar un tipo de trabajo")]
	public int TipoId { get; set; }

	[Required(ErrorMessage = "Favor llenar el campo monto")]
	public decimal Monto { get; set; }

	public Clientes Clientes { get; set; }
	public Tecnicos Tecnicos { get; set; }
	public TiposTecnicos TiposTecnicos { get; set; }
}
 