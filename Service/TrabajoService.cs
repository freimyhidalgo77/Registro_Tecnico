using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{
    public class TrabajoService
    {
        private readonly Context _context;

        public TrabajoService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Trabajos.AnyAsync(t => t.TrabajoId == id);
        }


        public async Task AfectarArticulos(TrabajosDetalle[] trabajosDetalle, bool afectar)
        {
            foreach (var detalle in trabajosDetalle)
            {
                var articulo = await _context.Articulos.SingleAsync(a => a.ArticuloId == detalle.ArticuloId);

                if (afectar)
                {
                    
                    articulo.existencia += detalle.cantidad; 
                }
                else
                {
                   
                    articulo.existencia -= detalle.cantidad;
                }

                
                _context.Articulos.Update(articulo);
            }

            await _context.SaveChangesAsync();
        }


        private async Task<bool> ValidarRelaciones(int clienteId, int tecnicoId, int tipoId, int prioridadId)
        {
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.ClienteId == clienteId);
            var tecnicoExiste = await _context.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
            var tipoExiste = await _context.TiposTecnicos.AnyAsync(tt => tt.TipoId == tipoId);
            var prioridadExiste = await _context.Prioridades.AnyAsync(p => p.PrioridadId == prioridadId);

            return clienteExiste && tecnicoExiste && tipoExiste && prioridadExiste;
        }
         
        private async Task<bool> Insertar(Trabajos trabajos)
        {
            if (await ValidarRelaciones(trabajos.ClienteId, trabajos.TecnicoId, trabajos.TipoId, trabajos.PrioridadId))
            {
                _context.Trabajos.Add(trabajos);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Modificar(Trabajos trabajos)
        {
           
              
                if (await ValidarRelaciones(trabajos.ClienteId, trabajos.TecnicoId, trabajos.TipoId, trabajos.PrioridadId))
                {
                   
                    var existingTrabajo = await _context.Trabajos
                    .Include(t => t.TrabajosDetalle)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                    if (existingTrabajo != null)
                    {
                    await AfectarArticulos(existingTrabajo.TrabajosDetalle.ToArray(), false);
                    await AfectarArticulos(trabajos.TrabajosDetalle.ToArray(),true);

                        _context.Update(trabajos);
                        return await _context.SaveChangesAsync() > 0;
                    }
                }
                return false;
            
           
        }


        public async Task<bool> Guardar(Trabajos trabajos)
        {
            if (!await Existe(trabajos.TrabajoId))
                return await Insertar(trabajos);
            else
                return await Modificar(trabajos);
        }

        public async Task<bool> Eliminar(int id)
        {
            var trabajosEliminados = await _context.Trabajos
                .Where(t => t.TrabajoId == id).ExecuteDeleteAsync();
            return trabajosEliminados > 0;
        }

        public async Task<Trabajos?> Buscar(int id)
        {
            return await _context.Trabajos
                .AsNoTracking()
                .Include(t => t.Clientes)
                .Include(t => t.Tecnicos)
                .Include(t => t.TiposTecnicos)
                .Select(t => new Trabajos
                {
                    TrabajoId = t.TrabajoId,
                    ClienteId = t.ClienteId,
                    TecnicoId = t.TecnicoId,
                    PrioridadId = t.PrioridadId,
                    TipoId = t.TipoId,
                    Fecha = t.Fecha,
                    Monto = t.Monto,

                    Clientes = new Clientes
                    {
                        ClienteId = t.Clientes.ClienteId,
                        NombreCliente = t.Clientes.NombreCliente
                    },
                    Tecnicos = new Tecnicos
                    {
                        TecnicoId = t.Tecnicos.TecnicoId,
                        NombreTecnico = t.Tecnicos.NombreTecnico
                    },
                    TiposTecnicos = new TiposTecnicos
                    {
                        Descripcion = t.TiposTecnicos.Descripcion
                    },
                    Prioridades = new Prioridades
                    {
                        descripcion = t.Prioridades.descripcion
                    },
                })
                .FirstOrDefaultAsync(t => t.TrabajoId == id);
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            return await _context.Trabajos
                .AsNoTracking()
                .Include(t => t.Clientes)
                .Include(t => t.Tecnicos)
                .Include(t => t.TiposTecnicos)
                .Include(t => t.Prioridades)
                .Where(criterio)
                .Select(t => new Trabajos
                {
                    TrabajoId = t.TrabajoId,
                    ClienteId = t.ClienteId,
                    TecnicoId = t.TecnicoId,
                    TipoId = t.TipoId,
                    Fecha = t.Fecha,
                    Monto = t.Monto,
                    PrioridadId = t.PrioridadId,
                    Clientes = new Clientes
                    {
                        ClienteId = t.Clientes.ClienteId,
                        NombreCliente = t.Clientes.NombreCliente
                    },
                    Tecnicos = new Tecnicos
                    {
                        TecnicoId = t.Tecnicos.TecnicoId,
                        NombreTecnico = t.Tecnicos.NombreTecnico
                    },
                    TiposTecnicos = new TiposTecnicos
                    {
                        Descripcion = t.TiposTecnicos.Descripcion
                    },
                    Prioridades = new Prioridades
                    {
                        descripcion = t.Prioridades.descripcion
                    },

                })
                .ToListAsync();
        }

        public async Task<Trabajos?> BuscarTrabajo(int trabajoId)
        {
            return await _context.Trabajos
                .AsNoTracking()
                .Include(t => t.Clientes)
                .Include(t => t.Tecnicos)
                .Include(t => t.TiposTecnicos)
                .Include(t => t.Prioridades)
                .Select(t => new Trabajos
                {
                    TrabajoId = t.TrabajoId,
                    ClienteId = t.ClienteId,
                    TecnicoId = t.TecnicoId,
                    TipoId = t.TipoId,
                    Fecha = t.Fecha,
                    Monto = t.Monto,
                    PrioridadId = t.PrioridadId,
                    Clientes = new Clientes
                    {
                        ClienteId = t.Clientes.ClienteId,
                        NombreCliente = t.Clientes.NombreCliente
                    },
                    Tecnicos = new Tecnicos
                    {
                        TecnicoId = t.Tecnicos.TecnicoId,
                        NombreTecnico = t.Tecnicos.NombreTecnico
                    },
                    TiposTecnicos = new TiposTecnicos
                    {
                        Descripcion = t.TiposTecnicos.Descripcion
                    },
                     Prioridades = new Prioridades
                     {
                         descripcion = t.Prioridades.descripcion
                     },
                })
                .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
        }



    }
}
