using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;


namespace RegistroTecnicos.DAL
{
    public class Context : DbContext
    {
        //Conexion con la base de datos (El contexto o context)
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
		public DbSet<Clientes> Clientes { get; set; }
		public DbSet<Trabajos> Trabajos { get; set; }

        public DbSet<Prioridades> Prioridades { get; set; } 


         

	}


} 

         