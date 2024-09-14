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

		private async Task<bool> ValidarRelaciones(int clienteId, int tecnicoId, int tipoId)
		{
			var clienteExiste = await _context.Clientes.AnyAsync(c => c.ClienteId == clienteId);
			var tecnicoExiste = await _context.Tecnicos.AnyAsync(t => t.TecnicoId == tecnicoId);
			var tipoExiste = await _context.TiposTecnicos.AnyAsync(tt => tt.TipoId == tipoId);

			return clienteExiste && tecnicoExiste && tipoExiste;
		}

		private async Task<bool> Insertar(Trabajos trabajos)
		{
			if (await ValidarRelaciones(trabajos.ClienteId, trabajos.TecnicoId, trabajos.TipoId))
			{
				_context.Trabajos.Add(trabajos);
				return await _context.SaveChangesAsync() > 0;
			}
			return false;
		}

		public async Task<bool> Modificar(Trabajos trabajos)
		{
			try
			{
				// Verificar las relaciones
				if (await ValidarRelaciones(trabajos.ClienteId, trabajos.TecnicoId, trabajos.TipoId))
				{
					// Asegúrate de que el contexto esté rastreando la entidad
					var existingTrabajo = await _context.Trabajos.FindAsync(trabajos.TrabajoId);
					if (existingTrabajo != null)
					{
						// Actualizar los valores de la entidad existente
						_context.Entry(existingTrabajo).CurrentValues.SetValues(trabajos);

						// Guardar cambios
						return await _context.SaveChangesAsync() > 0;
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				// Manejo de excepciones, por ejemplo, registrar el error
				Console.WriteLine($"Error al modificar el trabajo: {ex.Message}");
				return false;
			}
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
			var trabajosEliminados = await _context.Trabajos
				.Where(t => t.TrabajoId == id).ExecuteDeleteAsync();
			return trabajosEliminados > 0;
		}

		public async Task<Trabajos?> Buscar(int id)
		{
			return await _context.Trabajos
				.AsNoTracking()
				.Include(t => t.Clientes)
				.Include(t => t.Tecnicos)
				.Include(t => t.TiposTecnicos)
				.Select(t => new Trabajos
				{
					TrabajoId = t.TrabajoId,
					ClienteId = t.ClienteId,
					TecnicoId = t.TecnicoId,
					TipoId = t.TipoId,
					Fecha = t.Fecha,
					Monto = t.Monto,

					Clientes = new Clientes
					{
						ClienteId = t.Clientes.ClienteId,
						NombreCliente = t.Clientes.NombreCliente
					},
					Tecnicos = new Tecnicos
					{
						TecnicoId = t.Tecnicos.TecnicoId,
						NombreTecnico = t.Tecnicos.NombreTecnico
					},
					TiposTecnicos = new TiposTecnicos
					{
						Descripcion = t.TiposTecnicos.Descripcion
					}
				})
				.FirstOrDefaultAsync(t => t.TrabajoId == id);
		}

		public async Task<List<Trabajos>> Listar(Expression<Func<Trabajos, bool>> criterio)
		{
			return await _context.Trabajos
				.AsNoTracking()
				.Include(t => t.Clientes)
				.Include(t => t.Tecnicos)
				.Include(t => t.TiposTecnicos)
				.Where(criterio)
				.Select(t => new Trabajos
				{
					TrabajoId = t.TrabajoId,
					ClienteId = t.ClienteId,
					TecnicoId = t.TecnicoId,
					TipoId = t.TipoId,
					Fecha = t.Fecha,
					Monto = t.Monto,
					Clientes = new Clientes
					{
						ClienteId = t.Clientes.ClienteId,
						NombreCliente = t.Clientes.NombreCliente
					},
					Tecnicos = new Tecnicos
					{
						TecnicoId = t.Tecnicos.TecnicoId,
						NombreTecnico = t.Tecnicos.NombreTecnico
					},
					TiposTecnicos = new TiposTecnicos
					{
						Descripcion = t.TiposTecnicos.Descripcion
					}
				})
				.ToListAsync();
		}

		public async Task<Trabajos?> BuscarTrabajo(int trabajoId)
		{
			return await _context.Trabajos
				.AsNoTracking()
				.Include(t => t.Clientes)
				.Include(t => t.Tecnicos)
				.Include(t => t.TiposTecnicos)
				.Select(t => new Trabajos
				{
					TrabajoId = t.TrabajoId,
					ClienteId = t.ClienteId,
					TecnicoId = t.TecnicoId,
					TipoId = t.TipoId,
					Fecha = t.Fecha,
					Monto = t.Monto,
					Clientes = new Clientes
					{
						ClienteId = t.Clientes.ClienteId,
						NombreCliente = t.Clientes.NombreCliente
					},
					Tecnicos = new Tecnicos
					{
						TecnicoId = t.Tecnicos.TecnicoId,
						NombreTecnico = t.Tecnicos.NombreTecnico
					},
					TiposTecnicos = new TiposTecnicos
					{
						Descripcion = t.TiposTecnicos.Descripcion
					}
				})
				.FirstOrDefaultAsync(t => t.TrabajoId == trabajoId);
		}
	}
}
