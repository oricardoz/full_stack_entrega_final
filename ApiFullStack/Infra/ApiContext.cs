using System;
using ApiFullStack.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiFullStack.Infra;

public class ApiContext : DbContext
{

    public DbSet<Produtos> Produtos { get; set; } 

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Galpao> Galpoes { get; set; }

    public ApiContext()
    {
        caminho = @$"{AppDomain.CurrentDomain.BaseDirectory}\produtos.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={caminho}");
    }

    private string caminho;
}
