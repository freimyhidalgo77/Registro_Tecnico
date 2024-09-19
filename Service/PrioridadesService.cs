using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;


namespace RegistroTecnicos.Service
{
    public class PrioridadesService
    {
        private readonly Context _context;

        public PrioridadesService(Context context)
        {
            _context = context;

        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Prioridades.AnyAsync(t => t.PrioridadId == id);

        } 
         

        public async Task<bool> Insertar(Prioridades prioridades)
        {
            _context.Prioridades.Add(prioridades);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task <bool> Modificar(Prioridades prioridades)
        {
            _context.Prioridades.Update(prioridades);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task <bool> Guardar(Prioridades prioridades)
        {
            if(!await Existe(prioridades.PrioridadId))
                return await Insertar(prioridades);
            else
                return await Modificar(prioridades);

        }

        public async Task <bool> Eliminar(int id)
        {
            var Prioridades = await _context.Prioridades
            .Where(t => t.PrioridadId == id).ExecuteDeleteAsync();
            return Prioridades > 0;
        }

        public async Task<Prioridades?> Buscar(int id)
        {
            return await _context.Prioridades.AsNoTracking().
                FirstOrDefaultAsync(t => t.PrioridadId == id);
        }

        public async Task<List<Prioridades>>Listar(Expression <Func<Prioridades, bool>> criterio)
        {
            return _context.Prioridades.AsNoTracking()
                .Where(criterio)
                .ToList();

        }


        public async Task<Prioridades?> BuscarPrioridad(string descripcion)
        {
            return await _context.Prioridades.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Descripcion == descripcion);

        }


        public async Task<bool> PrioridadExiste(int id)
        {
            return await _context.Prioridades.AnyAsync(t => t.PrioridadId == id);

        }


    }
}
