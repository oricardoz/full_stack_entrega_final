using System;
using Microsoft.EntityFrameworkCore;


namespace ApiFullStack.Infra;

public class ApiContext : DbContext
{

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
