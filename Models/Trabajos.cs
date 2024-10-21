using RegistroTecnicos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Trabajos
{
    [Key]
    public int TrabajoId { get; set; }

    [Required(ErrorMessage = "Favor ingresar una fecha")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Campo descripcion obligatorio")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Favor llenar el campo monto")]
    public decimal Monto { get; set; }

    [ForeignKey("Clientes")]
    public int ClienteId { get; set; }
    public Clientes? Clientes { get; set; }


    [ForeignKey("Tecnicos")]
    public int TecnicoId { get; set; }
    public Tecnicos? Tecnicos { get; set; }


    [ForeignKey("TiposTecnicos")]
    public int TipoId { get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }


    [ForeignKey("Prioridades")]
    public int PrioridadId { get; set; }
    public Prioridades? Prioridades { get; set; }

 
    [ForeignKey("TrabajoId")]
    public ICollection<TrabajosDetalle> TrabajosDetalle { get; set; } = new List<TrabajosDetalle>();







}
