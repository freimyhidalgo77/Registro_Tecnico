using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{
    public class TrabajoService(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;

        private async Task<bool> Existe(int trabajoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Trabajos.AnyAsync(e => e.TrabajoId == trabajoId);
        }

        public async Task AfectarCantidad(TrabajosDetalle[] detalles, bool resta)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            foreach (var item in detalles)
            {
                var articulo = await context.Articulos.SingleAsync(a => a.ArticuloId == item.ArticuloId);
                if (resta)
                    articulo.existencia -= item.cantidad;
                else
                    articulo.existencia += item.cantidad;
            }
            await context.SaveChangesAsync();
        }

        private async Task<bool> Insertar(Trabajos trabajo)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            await AfectarCantidad(trabajo.TrabajosDetalle.ToArray(), true);
            context.Trabajos.Add(trabajo);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Trabajos trabajos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var trabajoOriginal = await context.Trabajos
            .Include(t => t.TrabajosDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TrabajoId == trabajos.TrabajoId);

            await AfectarCantidad(trabajoOriginal.TrabajosDetalle.ToArray(), false);

            await AfectarCantidad(trabajos.TrabajosDetalle.ToArray(), true);

            context.Update(trabajos);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Trabajos trabajo)
        {
            if (!await Existe(trabajo.TrabajoId))
                return await Insertar(trabajo);
            else
                return await Modificar(trabajo);
        }

        public async Task<bool> Eliminar(int trabajoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            var trabajo = context.Trabajos.Find(trabajoId);
            if (trabajo == null)
                return false;

            await AfectarCantidad(trabajo.TrabajosDetalle.ToArray(), false);
            return await context.Trabajos
                .Include(t => t.TrabajosDetalle)
                .Where(e => e.TrabajoId == trabajoId)
                .ExecuteDeleteAsync() > 0;
        }
        public async Task<Trabajos> Buscar(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Trabajos
                .Include(e => e.Tecnicos).Include(e => e.Clientes)
                .Include(e => e.Prioridades)
                .Include(t => t.TrabajosDetalle)
                .AsNoTracking()
               .FirstOrDefaultAsync(e => e.TrabajoId == id);
        }

        public async Task<Trabajos> BuscarDetalles(int trabajoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Trabajos
                .Include(t => t.Prioridades)
                .Include(t => t.Clientes)
                .Include(t => t.Tecnicos)
                .Include(t => t.TrabajosDetalle)
                .ThenInclude(td => td.Articulo)
                .FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
        }

        public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Trabajos.Include(e => e.Tecnicos)
                .Include(e => e.Clientes)
                .Include(e => e.Prioridades)
                .Include(t => t.TrabajosDetalle)
                .AsNoTracking().Where(criterio).ToListAsync();
        }

        public async Task<bool> BuscarTrabajo(int trabajoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Trabajos
                .AnyAsync(e => e.TrabajoId == trabajoId);
        }




    }
}
