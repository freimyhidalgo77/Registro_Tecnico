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
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<TrabajosDetalle> TrabajosDetalle { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
        {
            new Articulos(){ArticuloId=1, descripcion= "Tarjeta Grafica RTX 4060 ", costo= 100, precio= 165, existencia= 50},
            new Articulos(){ArticuloId=2, descripcion="Teclado Gaming K300 QWERY", costo= 225, precio= 275, existencia= 50},
            new Articulos(){ArticuloId=3, descripcion="Mouse Gamer  Logitech G502", costo= 25, precio= 35, existencia= 50},
            new Articulos(){ArticuloId=4, descripcion="Monitor Sony Inzone M9", costo= 30, precio= 38, existencia= 50}
        });
        }


    }

} 

         