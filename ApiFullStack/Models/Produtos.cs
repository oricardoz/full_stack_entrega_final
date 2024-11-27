using System;

namespace ApiFullStack.Models;

public class Produtos
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double ValorUnitario { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataCadastro { get; set; }
    public long GalpaoId { get; set; } 
}
