using System;
using ApiFullStack.Dtos;
using ApiFullStack.Models;
using ApiFullStack.Services;
using ApiFullStack.Infra;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApiFullStack.Endpoints;

public static class LoginEndpoint
{
    public static void AdicionarLoginEndpoint(this WebApplication app)
    {
        var grupo = app.MapGroup("/login");

        grupo.MapPost("/app", PostLoginAppAsync);
        grupo.MapPost("/navegador", PostLoginNavegadorAsync);
        grupo.MapGet("/logout", GetLogoutNavegador);
    }

    private static async Task<IResult> PostLoginAppAsync(LoginDTO infoLogin, ApiContext db, IPasswordHasher<Usuario> hasher)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Email == infoLogin.Email);

        if (usuario == null)
            return TypedResults.Unauthorized();
        else if (hasher.VerifyHashedPassword(usuario, usuario.HashSenha, infoLogin.HashSenha) == PasswordVerificationResult.Failed)
            return TypedResults.Unauthorized();

        return TypedResults.Ok(new TokenService().Gerar(usuario));
    }

    private static async Task<IResult> PostLoginNavegadorAsync(LoginDTO infoLogin, ApiContext db, IPasswordHasher<Usuario> hasher, HttpContext contexto)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(x => x.Email == infoLogin.Email);

        if (usuario == null)
            return TypedResults.Unauthorized();

        var token = new TokenService().Gerar(usuario);

        contexto.Response.Cookies.Append(
            "acessToken",
            token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

        return TypedResults.Ok(usuario); // Return the user data
    }

    private static IResult GetLogoutNavegador(HttpContext contexto)
    {
        contexto.Response.Cookies.Delete("acessToken");

        return TypedResults.Ok();
    }
}