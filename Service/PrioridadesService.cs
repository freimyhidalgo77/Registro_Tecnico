using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;


namespace RegistroTecnicos.Service
{
    public class PrioridadesService(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;

        private async Task<bool> Existe(int prioridadId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Prioridades.AnyAsync(e => e.PrioridadId == prioridadId);
        }

        private async Task<bool> Insertar(Prioridades prioridad)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Prioridades.Add(prioridad);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prioridades prioridad)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Update(prioridad);
            var modificado = await context.SaveChangesAsync() > 0;
            return modificado;
        }

        public async Task<bool> Guardar(Prioridades prioridad)
        {
            if (!await Existe(prioridad.PrioridadId))
                return await Insertar(prioridad);
                return await Modificar(prioridad);   
        }

        public async Task<bool> Eliminar(int prioridadId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Prioridades
                .Where(e => e.PrioridadId == prioridadId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<Prioridades> Buscar(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Prioridades
                .FirstOrDefaultAsync(e => e.PrioridadId == id);
        }

        public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Prioridades
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> PrioridadExiste(int id, int tiempo, string descripcion)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Prioridades
                .AnyAsync(e => e.PrioridadId != id
                && e.Tiempo == tiempo
                || e.descripcion.ToLower().Equals(descripcion.ToLower()));
        }


    }
}
