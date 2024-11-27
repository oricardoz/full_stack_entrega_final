using System;
using ApiFullStack.Models;

namespace ApiFullStack.Dtos;

public class ProdutoDTO
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public double ValorUnitario { get; set; }

    public double ValorTotal { get; set; }

    public string DataCadastro { get; set; }

    public ProdutoDTO(Produtos obj)
    {
        Id = obj.Id.ToString();
        Nome = obj.Nome;
        Quantidade = obj.Quantidade;
        ValorUnitario = obj.ValorUnitario;
        ValorTotal = obj.Quantidade * obj.ValorUnitario;
        DataCadastro = obj.DataCadastro.ToString();
    }

    public void PreencherModel(Produtos obj)
    {
        long.TryParse(this.Id, out long id);
        obj.Id = id;
        obj.Nome = this.Nome;
        obj.Quantidade = this.Quantidade;
        obj.ValorUnitario = this.ValorUnitario;

    }

    public Produtos GetModel()
    {
        var obj = new Produtos();
        obj.DataCadastro = DateTime.Now;
        PreencherModel(obj);
        return obj;
    }
}
