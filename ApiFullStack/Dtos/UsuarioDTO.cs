using System;
using ApiFullStack.Models;

namespace ApiFullStack.Dtos;

public class UsuarioDTO
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string HashSenha { get; set; } = string.Empty;

    public UsuarioDTO() {}

    public UsuarioDTO(Usuario obj)
    {
        Id = obj.Id.ToString();
        Nome = obj.Nome;
        Email = obj.Email;
        HashSenha = obj.HashSenha;
    }
    public Usuario GetModel()
    {
        var obj = new Usuario();
        PreencherModel(obj);
        return obj;
    }

    public void PreencherModel(Usuario obj)
    {
        long.TryParse(this.Id, out long id);
        obj.Id = id;
        obj.Nome = this.Nome;
        obj.Email = this.Email;
        obj.HashSenha = this.HashSenha;
    }

}

