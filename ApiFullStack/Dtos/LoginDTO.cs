using System;

namespace ApiFullStack.Dtos;


public class LoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string HashSenha { get; set; } = string.Empty;
    
}
