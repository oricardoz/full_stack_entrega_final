using System;

namespace ApiFullStack;

public class DotEnv
{
    public void CarregarCaminho(string caminho) 
    {
        if(!File.Exists(caminho))
            return;

        var linhas = File.ReadAllLines(caminho);

        foreach(var linha in linhas)
        {
            var partes = linha.Split("=");
            if(partes.Length != 2)
                continue;

            var chave = partes[0];
            var valor = partes[1];

            Environment.SetEnvironmentVariable(chave, valor);
        }
    }

}
