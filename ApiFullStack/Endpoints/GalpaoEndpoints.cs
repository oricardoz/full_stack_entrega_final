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
        return TypedResults.Ok(obj.Select(x => new GalpaoDTO(x)));
    }

    private static async Task<IResult> GetByIdAsync(string id, ApiContext db)
    {
        var obj = await db.Galpoes.Include(g => g.Produtos).FirstOrDefaultAsync(g => g.Id == Convert.ToInt64(id));  

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GalpaoDTO(obj));
    }

    private static async Task<IResult> PostAsync(ApiContext db, GalpaoDTO dto)

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

    private static async Task<IResult> PutAsync(string id, ApiContext db, GalpaoDTO dto)
    {
        if (!long.TryParse(id, out var longId))
        {
            return TypedResults.BadRequest("Invalid ID format");
        }

        var obj = await db.Galpoes.FindAsync(longId);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);

        db.Update(obj);
        obj.Id = longId;
        await db.SaveChangesAsync();

        return TypedResults.Ok(new GalpaoDTO(obj));
    }

    private static async Task<IResult> DeleteAsync(string id, ApiContext db)
    {
        if (!long.TryParse(id, out var longId))
        {
            return TypedResults.BadRequest("Invalid ID format");
        }

        var obj = await db.Galpoes.FindAsync(longId);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Galpoes.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent(); 
    }
}