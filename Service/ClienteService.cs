using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;


namespace RegistroTecnicos.Service
{
	public class ClienteService
	{

		private readonly Context _context;

		public ClienteService(Context context)
		{
			_context = context;
		}

		public async Task<bool> Existe(int id)
		{
			return await _context.Clientes.AnyAsync(t => t.ClienteId == id);
		}

		private async Task<bool> Insertar(Clientes clientes)
		{
			_context.Clientes.Add(clientes);
			return await _context.SaveChangesAsync() > 0;
		}

		private async Task<bool> Modificar(Clientes clientes)
		{
			_context.Clientes.Update(clientes);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Guardar(Clientes clientes)
		{
			if (!await Existe(clientes.ClienteId))
				return await Insertar(clientes);
			else
				return await Modificar(clientes);
		}

		public async Task<bool> Eliminar(int id)
		{
			var Incentivos = await _context.Clientes
				.Where(t => t.ClienteId == id).ExecuteDeleteAsync();
			return Incentivos > 0;
		}

		public async Task<Clientes?> Buscar(int id)
		{
			return await _context.Clientes.AsNoTracking().
				FirstOrDefaultAsync(t => t.ClienteId == id);
		}

		public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
		{
			return _context.Clientes.AsNoTracking()
				.Where(criterio)
				.ToList();
		}

		//Filtrar cliente
		public async Task<Clientes>? BuscarCliente(string nombre)
		{
			return await _context.Clientes.AsNoTracking()
				.FirstOrDefaultAsync(t => t.NombreCliente == nombre);
		}

		public async Task<bool> ClienteExiste(string nombreCliente)
		{
			return await _context.Clientes.AnyAsync(t => t.NombreCliente == nombreCliente);

		}



	}
}
