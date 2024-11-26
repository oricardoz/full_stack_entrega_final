using System;
using ApiFullStack.Dtos;
using Microsoft.EntityFrameworkCore;
using ApiFullStack.Infra;
using ApiFullStack.Models;
using Microsoft.AspNetCore.Identity;
using ApiFullStack.Services;

namespace ApiFullStack.Endpoints;

public static class UsuariosEndpoints
{
    static UsuariosEndpoints() {}

    public static void AdicionarEndpointUsuarios(this WebApplication app)
    {
        var grupo = app.MapGroup("/usuarios");

        grupo.MapGet("/", GetAsync );
        grupo.MapGet("/{Id}", GetByIdAsync );
        grupo.MapPost("/", PostAsync );
        grupo.MapPost("/admin", PostAsyncAdmin );
        grupo.MapPost("/login", LoginAsync);
        grupo.MapDelete("/{Id}", DeleteAsync );
        grupo.MapPut("/{Id}", PutAsync );

    }

    private static async Task<IResult> GetAsync(ApiContext db)
    {
        var obj = await db.Usuarios.ToListAsync();
        return TypedResults.Ok(obj.Select(x => new UsuarioDTO(x)));
    }

    private static async Task<IResult> GetByIdAsync(string id, ApiContext db)
    {
        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        return TypedResults.Ok(new UsuarioDTO(obj)); 
    }

     private static async Task<IResult> PostAsync(UsuarioDTO dto, ApiContext db)
    {
        Usuario obj = dto.GetModel();
        obj.Id =  GeradorId.GetId();
        obj.Role = "comum";
        await db.Usuarios.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"usuarios/{obj.Id}", new UsuarioDTO(obj));

    }

        private static async Task<IResult> PostAsyncAdmin(UsuarioDTO dto, ApiContext db)
    {
        Usuario obj = dto.GetModel();
        obj.Id =  GeradorId.GetId();
        obj.Role = "admin";
        await db.Usuarios.AddAsync(obj);
        await db.SaveChangesAsync();

        return TypedResults.Created($"usuarios/{obj.Id}", new UsuarioDTO(obj));

    }

    private static async Task<IResult> PutAsync(string id, UsuarioDTO dto, ApiContext db)
    {
        if(id != dto.Id)
            return TypedResults.BadRequest();

        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        dto.PreencherModel(obj);

        db.Update(obj);
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteAsync(string id, ApiContext db)
    {
        var obj = await db.Usuarios.FindAsync(Convert.ToInt64(id));

        if(obj == null)
            return TypedResults.NotFound();

        db.Usuarios.Remove(obj);   
        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> LoginAsync(LoginDTO dto, ApiContext db, TokenService tokenService)
    {
        var user = await db.Usuarios.SingleOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || user.HashSenha != dto.HashSenha)
        {
            return TypedResults.Unauthorized();
        }

        var token = tokenService.Gerar(user);
        return TypedResults.Ok(new { Token = token });
    }
}
