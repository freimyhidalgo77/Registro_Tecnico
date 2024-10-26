using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{
    public class CotizacionesDetalleService(IDbContextFactory<Context> DbFactory)
    {
        private readonly Context _context;

		public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
		{
			await using var context = await DbFactory.CreateDbContextAsync();
			return await context.Articulos.Where(criterio).ToListAsync();
		}

	}
}
