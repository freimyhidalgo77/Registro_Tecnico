using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Service
{
    public class TrabajosDetalleService(IDbContextFactory<Context> DbFactory)
    {
    
        private readonly Context _context;

        public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
        {
            await using var context = await DbFactory.CreateDbContextAsync();
            return await context.Articulos.Where(criterio).ToListAsync();
        }


    }
}    
