using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
/*using System.Security.Cryptography.X509Certificates;*/

namespace RegistroTecnicos.Service
{
	public class TecnicoService
	{
		
		private readonly Context _context;

		//Se llama al context
		public TecnicoService(Context context)
		{
			_context = context;
			 
		}
		  
		//Metodo para verificar si el tecnico exite
		public async Task<bool> Existe(int id)
		{
			return await _context.Tecnicos.AnyAsync(t => t.TecnicoId == id);
		}

		//Metodo para modificar el tecnico ya existente
		public async Task<bool> Modificar(Tecnicos tecnico)
		{
		    _context.Tecnicos.Update(tecnico);
			return await _context.SaveChangesAsync() > 0;
		}

		//Metodo para agregar un tecnico
		public async Task<bool> Insertar(Tecnicos tecnico)
		{
			_context.Tecnicos.Add(tecnico);
			return await _context.SaveChangesAsync() > 0;

		}

		//Metodo para Guardar un tecnico
		public async Task<bool> Guardar(Tecnicos tecnico)
		{
			if(!await Existe(tecnico.TecnicoId))
				return await Insertar(tecnico);
			return await Modificar(tecnico);
		}

		//Metodo para eliminar un tecnico guardado
		public async Task<bool> Eliminar(int id)
		{
			var Tecnicos = await _context.Tecnicos.FirstOrDefaultAsync(t => t.TecnicoId == id);
			if(Tecnicos != null)
			{
				_context.Tecnicos.Remove(Tecnicos);
				return await _context.SaveChangesAsync() > 0;

			}

			return false; //Si no se encuetra el tecnico retorna a falso

		}

		public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos,bool>> pauta)
		{
			return _context.Tecnicos.AsNoTracking()
				.Where(pauta)
				.ToList();

		}

		//Metodo para filtrar un tecnico por nombre
		public async Task<Tecnicos?> BuscarNombres( string nombre)
		{
			return await _context.Tecnicos.AsNoTracking()
				.FirstOrDefaultAsync(t => t.NombreTecnico == nombre);
				
		}

		//Metodo para buscar tecnico
		public async Task<Tecnicos> Buscar(int id)
		{
			return await _context.Tecnicos.AsNoTracking().
				FirstOrDefaultAsync(t => t.TecnicoId == id);

		}

		public async Task<bool> ValidarTecnico(string nombre)
		{
			return await _context.Tecnicos.AnyAsync(t => t.NombreTecnico == nombre);
		}


	}
}
