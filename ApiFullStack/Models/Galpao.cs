using System;
using System.Collections.Generic;

namespace ApiFullStack.Models;

public class Galpao
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public List<Produtos> Produtos { get; set; } = new List<Produtos>(); 
}
