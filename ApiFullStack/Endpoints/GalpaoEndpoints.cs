
using System;
using ApiFullStack.Dtos;
using ApiFullStack.Infra;
using ApiFullStack.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFullStack.Endpoints;

public static class GalpaoEndpoints
{
    static GalpaoEndpoints() { }

    public static void AdicionarEndpointsGalpoes(this WebApplication app)
    {
        var grupo = app.MapGroup("/galpoes").RequireAuthorization();

        grupo.MapGet("/", GetAsync);                  
        grupo.MapGet("/{id}", GetByIdAsync);    
        grupo.MapPost("/", PostAsync);                 
        grupo.MapPut("/{id}", PutAsync);             
        grupo.MapDelete("/{id}", DeleteAsync);      
    }

    private static async Task<IResult> GetAsync(ApiContext db)
    {
        var obj = await db.Galpoes.Include(g => g.Produtos).ToListAsync();
        return TypedResults.Ok(obj);
    }

    private static async Task<IResult> GetByIdAsync(long id, ApiContext db)
    {
        var obj = await db.Galpoes.Include(g => g.Produtos).FirstOrDefaultAsync(g => g.Id == id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GalpaoDTO(obj));
    }

    private static async Task<IResult> PostAsync(ApiContext db, GalpaoDTO dto)
{
    var obj = dto.GetModel();

    if (dto.Produtos == null || !dto.Produtos.Any())
    {
        obj.Produtos = new List<Produtos>();
    }

    obj.Id = GeradorId.GetId(); 
    await db.Galpoes.AddAsync(obj);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/galpoes/{obj.Id}", new GalpaoDTO(obj));
}

    private static async Task<IResult> PutAsync(long id, ApiContext db, GalpaoDTO dto)
    {
        var obj = await db.Galpoes.FindAsync(id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);
        obj.Id = id;
        await db.SaveChangesAsync();

        return TypedResults.Ok(new GalpaoDTO(obj));
    }

    private static async Task<IResult> DeleteAsync(long id, ApiContext db)
    {
        var obj = await db.Galpoes.FindAsync(id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Galpoes.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent(); 
    }
}