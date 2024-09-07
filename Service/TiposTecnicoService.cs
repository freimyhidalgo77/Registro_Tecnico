using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Service
{

    public class TiposTecnicoService
    {
        private readonly Context _context;

        public TiposTecnicoService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.TiposTecnicos.AnyAsync(t => t.TipoId == id);
        }

        public async Task<bool> Insertar(TiposTecnicos tecnico)
        {
            _context.TiposTecnicos.Add(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> Modificar(TiposTecnicos tecnico)
        {
            _context.TiposTecnicos.Update(tecnico); 
            return await _context.SaveChangesAsync() > 0;

        }


        public async Task<bool> Guardar(TiposTecnicos tecnico)
        {
            if (!await Existe(tecnico.TipoId))
                return await Insertar(tecnico);
            else
                return await Modificar(tecnico);

        }

        public async Task<bool> Eliminar(int id)
        {
            var tipo = await _context.TiposTecnicos
                .Where(t => t.TipoId == id).ExecuteDeleteAsync();
            return tipo > 0;
        }

        public async Task<TiposTecnicos?> Buscar(int id)
        {
            return await _context.TiposTecnicos.AsNoTracking().
                FirstOrDefaultAsync(t => t.TipoId == id);

        }

        public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
        {
            return _context.TiposTecnicos.AsNoTracking()
                .Where(criterio)
                .ToList();

        }


        public async Task<List<TiposTecnicos>> ListarTiposTecnicos()
        {
            return await _context.Tecnicos
                .AsNoTracking()
                .Select(t => t.TipoTecnicos)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> TipoTecnicoDescripExiste(string descripcion)
        {
            return await _context.TiposTecnicos.AnyAsync(t => t.Descripcion == descripcion);

        }

    }

}


