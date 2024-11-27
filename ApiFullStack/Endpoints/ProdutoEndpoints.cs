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
        var grupo = app.MapGroup("/produtos");

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
        return TypedResults.Ok(obj);
    }

    private static async Task<IResult> GetByIdAsync(long id, ApiContext db)
    {
        var obj = await db.Produtos.FindAsync(id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new ProdutoDTO(obj));
    }

    private static async Task<IResult> PostAsync(ApiContext db, ProdutoDTO dto)
    {
        Produtos obj = dto.GetModel();
        obj.Id = GeradorId.GetId(); // Gerar ID único
        await db.Produtos.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/produtos/{obj.Id}", new ProdutoDTO(obj));
    }

    private static async Task<IResult> PutAsync(long id, ApiContext db, ProdutoDTO dto)
    {
        var obj = await db.Produtos.FindAsync(id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        dto.PreencherModel(obj);
        obj.Id = id; // Garantir que o ID não seja alterado
        await db.SaveChangesAsync();

        return TypedResults.Ok(new ProdutoDTO(obj));
    }

    private static async Task<IResult> DeleteAsync(long id, ApiContext db)
    {
        var obj = await db.Produtos.FindAsync(id);

        if (obj == null)
        {
            return TypedResults.NotFound();
        }

        db.Produtos.Remove(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent(); // Deletado com sucesso
    }
}