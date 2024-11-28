using System;
using ApiFullStack.Dtos;
using ApiFullStack.Infra;
using ApiFullStack.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFullStack.Endpoints;

public static class ProdutoEndpoints
{
    static ProdutoEndpoints() { }

    public static void AdicionarEndpointsProdutos(this WebApplication app)
    {
        var grupo = app.MapGroup("/produtos").RequireAuthorization();

        // Operações de CRUD
        grupo.MapGet("/", GetAsync);                  
        grupo.MapGet("/{id}", GetByIdAsync);    
        grupo.MapPost("/", PostAsync);                 
        grupo.MapPut("/{id}", PutAsync);             
        grupo.MapDelete("/{id}", DeleteAsync);      
    }

    private static async Task<IResult> GetAsync(ApiContext db)
    {
        var obj = await db.Produtos.ToListAsync();
        return TypedResults.Ok(obj.Select(x => new ProdutoDTO(x)));
    }

    private static async Task<IResult> GetByIdAsync(string id, ApiContext db)
    {
        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new ProdutoDTO(obj));
    }

    private static async Task<IResult> PostAsync(ApiContext db, ProdutoDTO dto)
{
    if (!long.TryParse(dto.GalpaoId, out long galpaoId))
    {
        return TypedResults.BadRequest("O Galpão especificado não é válido.");
    }

    var galpao = await db.Galpoes.FindAsync(galpaoId);

    if (galpao == null)
    {
        return TypedResults.BadRequest("O Galpão especificado não existe.");
    }

    Produtos obj = dto.GetModel();
    obj.Id = GeradorId.GetId();
    obj.GalpaoId = galpaoId;

    await db.Produtos.AddAsync(obj);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/produtos/{obj.Id}", new ProdutoDTO(obj));
}    private static async Task<IResult> PutAsync(string id, ApiContext db, ProdutoDTO dto)
    {
        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);
        obj.Id = Convert.ToInt64(id);
        db.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.Ok(new ProdutoDTO(obj));
    }

    private static async Task<IResult> DeleteAsync(string id, ApiContext db)
    {
        var obj = await db.Produtos.FindAsync(Convert.ToInt64(id));

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Produtos.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent(); 
    }
}
