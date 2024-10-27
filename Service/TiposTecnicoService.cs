using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{

    public class TiposTecnicoService(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;

        private async Task<bool> Existe(int tiposTecnicosId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.TiposTecnicos.AnyAsync(e => e.TipoId == tiposTecnicosId);
        }

        private async Task<bool> Insertar(TiposTecnicos tiposTecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.TiposTecnicos.Add(tiposTecnicos);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(TiposTecnicos tiposTecnicos)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            context.Update(tiposTecnicos);
            var modificado = await context.SaveChangesAsync() > 0;
            return modificado;
        }

        public async Task<bool> Guardar(TiposTecnicos tiposTecnicos)
        {
            if (!await Existe(tiposTecnicos.TipoId))
                return await Insertar(tiposTecnicos);
            else
                return await Modificar(tiposTecnicos);
        }

        public async Task<bool> Eliminar(int tipoTecnicoId)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.TiposTecnicos
                .Where(e => e.TipoId == tipoTecnicoId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<TiposTecnicos> Buscar(int id)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.TiposTecnicos
                .FirstOrDefaultAsync(e => e.TipoId == id);
        }

        public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.TiposTecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> ExisteTipo(int id, string descripcion)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.TiposTecnicos
                .AnyAsync(e => e.TipoId != id
                && e.Descripcion.ToLower().Equals(descripcion.ToLower()));
        }

    }

}


