using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{
    public class CotizacionService(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;

		private async Task<bool> Existe(int cotizacionId)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			return await context.Cotizaciones.AnyAsync(c => c.CotizacionId == cotizacionId);
		}

		private async Task<bool> Insertar(Cotizaciones cotizacion)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			context.Cotizaciones.Add(cotizacion);
			return await context.SaveChangesAsync() > 0;
		}

		private async Task<bool> Modificar(Cotizaciones cotizacion)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			context.Cotizaciones.Update(cotizacion);
			var modificado = await context.SaveChangesAsync() > 0;
			return modificado;
		}

		public async Task<bool> Guardar(Cotizaciones cotizacion)
		{
			if (!await Existe(cotizacion.CotizacionId))
				return await Insertar(cotizacion);
			else
				return await Modificar(cotizacion);
		}

		public async Task<bool> Eliminar(int cotizacionId)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			return await context.Cotizaciones
				.Include(d => d.CotizacionesDetalles)
				.Where(c => c.CotizacionId == cotizacionId)
				.ExecuteDeleteAsync() > 0;
		}

		public async Task<Cotizaciones> Buscar(int id)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			return await context.Cotizaciones
				.Include(d => d.CotizacionesDetalles)
				.FirstOrDefaultAsync(c => c.CotizacionId == id);
		}
		public async Task<Cotizaciones> BuscarConDetalles(int Id)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			return await context.Cotizaciones
				.Include(t => t.Clientes)
				.Include(t => t.CotizacionesDetalles)
				.ThenInclude(td => td.Articulos)
				.FirstOrDefaultAsync(t => t.CotizacionId == Id);
		}
		public async Task<List<Cotizaciones>> Listar(Expression<Func<Cotizaciones, bool>> criterio)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			return await contexto.Cotizaciones
				.Include(c => c.Clientes)
				.Include(d => d.CotizacionesDetalles)
				.AsNoTracking()
				.Where(criterio)
				.ToListAsync();
		}


	}
}
