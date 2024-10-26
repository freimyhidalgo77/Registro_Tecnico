using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace RegistroTecnicos.Service;

public class TecnicoService(IDbContextFactory<Context> DbFactory)
{

    private readonly Context _context;

    //Metodo para verificar si el tecnico exite
    private async Task<bool> Existe(int tecnicoId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Tecnicos.AnyAsync(e => e.TecnicoId == tecnicoId);
    }

    //Metodo para modificar el tecnico ya existente
    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        context.Update(tecnico);
        var modificado = await context.SaveChangesAsync() > 0;
        return modificado;
    }

    //Metodo para agregar un tecnico
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        context.Tecnicos.Add(tecnico);
        return await context.SaveChangesAsync() > 0;
    }

    //Metodo para Guardar un tecnico
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);
    }

    //Metodo para eliminar un tecnico guardado

    public async Task<bool> Eliminar(int tecnicoId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Tecnicos.
            Where(e => e.TecnicoId == tecnicoId).ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Tecnicos
            .Include(t => t.TipoTecnicos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

	//Metodo para filtrar un tecnico por nombre
	/* public async Task<Tecnicos?> BuscarTecnico(int id)
	 {
		 return await _context.Tecnicos.AsNoTracking()
			 .FirstOrDefaultAsync(t => t.TecnicoId == id);

	 }*/

	//Metodo para buscar tecnico

	public async Task<Tecnicos> BuscarTecnico(string nombre)
	{
		await using var context = await DbFactory.CreateDbContextAsync();
		return await context.Tecnicos
			.Include(t => t.TipoTecnicos)
			.FirstOrDefaultAsync(e => e.NombreTecnico == nombre);
	}
	public async Task<Tecnicos> Buscar(int id)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Tecnicos
            .Include(t => t.TipoTecnicos)
            .FirstOrDefaultAsync(e => e.TecnicoId == id);
    }
    public async Task<bool> ValidarTecnico(string nombre)
    {
        return await _context.Tecnicos.AnyAsync(t => t.NombreTecnico == nombre);
    }

    public async Task<List<TiposTecnicos>> ListarTiposTecnicos()
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .Select(t => t.TipoTecnicos)
            .Distinct()
            .ToListAsync();
    }


}
