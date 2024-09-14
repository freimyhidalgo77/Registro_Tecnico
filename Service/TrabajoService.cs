using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;


namespace RegistroTecnicos.Service
{
	public class TrabajoService
	{

		private readonly Context _context;

		public TrabajoService(Context context)
		{
			_context = context;
		}

		public async Task<bool> Existe(int id)
		{
			return await _context.Trabajos.AnyAsync(t => t.TrabajoId == id);
		}

		private async Task<bool> Insertar(Trabajos trabajos)
		{
			_context.Trabajos.Add(trabajos);
			return await _context.SaveChangesAsync() > 0;
		}

		private async Task<bool> Modificar(Trabajos trabajos)
		{
			_context.Trabajos.Update(trabajos);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Guardar(Trabajos trabajos)
		{
			if (!await Existe(trabajos.TrabajoId))
				return await Insertar(trabajos);
			else
				return await Modificar(trabajos);
		}

		public async Task<bool> Eliminar(int id)
		{
			var Trabajos = await _context.Trabajos
				.Where(t => t.TrabajoId == id).ExecuteDeleteAsync();
			return Trabajos > 0;
		}

		public async Task<Trabajos?> Buscar(int id)
		{
			return await _context.Trabajos.AsNoTracking().
				FirstOrDefaultAsync(t => t.TrabajoId == id);
		}

		public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
		{
			return _context.Trabajos.AsNoTracking()
				.Where(criterio)
				.ToList();
		}

		//Filtrar cliente
		public async Task<Trabajos>? BuscarTrabajo(int TrabajoId)
		{
			return await _context.Trabajos.AsNoTracking()
				.FirstOrDefaultAsync(t => t.TrabajoId == TrabajoId);
		}

	}


}
