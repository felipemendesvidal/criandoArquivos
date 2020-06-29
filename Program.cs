using System;

namespace criandoarquivos
{
    class Program
    {
        static void Main(string[] args)
        {
            Base teste = new Base();
            teste.Codigo = 1;
            teste.Nome = "teste";
            teste.Preco = 123f;

            teste.Cadastrar(teste);
        }
    }
}
