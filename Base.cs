using System.IO;
namespace criandoarquivos
{
    public class Base
    {
        //declarações
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        /*Desafio
        *Criando repositorio
        string pasta = PATH.Split('/')[0];
        if(!Directory.Exists(pasta)){
            Directory.CreateDirectory(pasta);
        }
        */

        //constante que vai receber caminho do database
        //constantes sempre com letras maiusculas
        private const string PATH ="Database/dbase.csv";

        //cria o arquivo caso não exista
        public Base(){
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        //adiciona coisas no arquivo criado
        public void Cadastrar (Base prod){
            var linha = new string[] {
                PrepararLinha (prod)
                };
                File.AppendAllLines(PATH, linha);
        }
    //linha do doc
    private string PrepararLinha(Base lp){
        return $"Codigo={lp.Codigo};nome={lp.Nome};preco={lp.Preco};";
    }

        
    }
}