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
        public async Task<bool> Agregar(int articuloId, int cantidad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            if (cantidad <= 0)
            {
                throw new ArgumentException("Error, la cantidad debe ser mayor que cero.");
            }

            var articulo = await contexto.Articulos.FindAsync(articuloId);

            if (articulo != null)
            {
                articulo.existencia += cantidad;
                contexto.Articulos.Update(articulo);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }
}
