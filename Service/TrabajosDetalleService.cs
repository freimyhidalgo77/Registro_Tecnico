using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Service
{
    public class TrabajosDetalleService
    {

        private readonly Context _contexto;

        public TrabajosDetalleService(Context context)
        {
            _contexto = context;
        }


        public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
        {
            return await _contexto.Articulos.AsNoTracking().Where(criterio).ToListAsync();
        }


    }
}    
