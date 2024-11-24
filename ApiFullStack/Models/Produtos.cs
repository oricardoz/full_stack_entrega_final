using System;

namespace ApiFullStack.Models;

public class Produtos
{
    public long Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public double Preco { get; set; }

    public int Quantidade { get; set; }

}
