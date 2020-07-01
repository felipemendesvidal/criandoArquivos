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

        
        /// cria o arquivo caso não exista
        public Base(){
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        ///adiciona coisas no arquivo criado
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

    //Le o arquivo svg e retorna lista 
    public List<Base> Ler(){
        //lista que seriva de retorno
        List<Base> produtos = new List<Base>();

        //lendo o arquivo e tranformando em array de linhas- pesquisar mais array
        string[] linhas =File.ReadAllLines(PATH);

        foreach(string linha in linhas){
            //separando os dados
            string[] dados = linha.Split(";");

            //criando produtos para colocar na lista
            Base pro = new Base();
            pro.Codigo = Int32.Parse(SepararDados( dados[0]));
            pro.Nome = SepararDados(dados[1]);
            pro.Preco = float.Parse(SepararDados(dados[2]));

            //adicionando a lista
            produtos.Add(pro);
        }
        return produtos;
    }

    //separa dados em colunas
    private string SepararDados(string coluna){

            return coluna.Split("=")[1];        
        }

        
    }
}