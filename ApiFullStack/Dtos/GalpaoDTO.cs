
using System;
using System.Collections.Generic;
using System.Linq;
using ApiFullStack.Models;

namespace ApiFullStack.Dtos;

public class GalpaoDTO
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public List<ProdutoDTO>? Produtos { get; set; } // Permite null

    public GalpaoDTO() { }

    public GalpaoDTO(Galpao obj)
    {
        Id = obj.Id.ToString();
        Nome = obj.Nome;
        Endereco = obj.Endereco;
        Produtos = obj.Produtos?.Select(p => new ProdutoDTO(p)).ToList() ?? new List<ProdutoDTO>();
    }

    public void PreencherModel(Galpao obj)
    {
        obj.Id = long.TryParse(Id, out long id) ? id : 0; 
        obj.Nome = Nome;
        obj.Endereco = Endereco;
        obj.Produtos = Produtos?.Select(p => p.GetModel()).ToList() ?? new List<Produtos>();
    }

    public Galpao GetModel()
    {
        var obj = new Galpao();
        PreencherModel(obj);
        return obj;
    }
}
