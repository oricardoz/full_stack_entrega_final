using System;
using ApiFullStack.Models;

namespace ApiFullStack.Dtos;

public class ProdutoDTO
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double ValorUnitario { get; set; }
    public double ValorTotal => Quantidade * ValorUnitario; 
    public string DataCadastro { get; set; } = string.Empty;
    public string GalpaoId { get; set; }

    // Construtor vazio para o System.Text.Json
    public ProdutoDTO() { }

    // Construtor customizado
    public ProdutoDTO(Produtos obj)
    {
        Id = obj.Id.ToString();
        Nome = obj.Nome;
        Quantidade = obj.Quantidade;
        ValorUnitario = obj.ValorUnitario;
        DataCadastro = obj.DataCadastro.ToString("yyyy-MM-dd HH:mm:ss");
        GalpaoId = obj.GalpaoId.ToString(); 
    }
    public void PreencherModel(Produtos obj)
    {
        obj.Id = long.TryParse(Id, out long id) ? id : 0; 
        obj.Nome = Nome;
        obj.Quantidade = Quantidade;
        obj.ValorUnitario = ValorUnitario;
        obj.GalpaoId = long.TryParse(GalpaoId, out long galpaoId) ? galpaoId : 0;
    }

    public Produtos GetModel()
    {
        var obj = new Produtos
        {
            DataCadastro = DateTime.Now
        };
        PreencherModel(obj);
        return obj;
    }
}
