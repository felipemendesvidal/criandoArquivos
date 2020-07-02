using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
namespace crudRecriado
{
    public class Produtos
    {
        //propriedades
        public int Codigo{get; set;}
        public string Nome{get; set;}
        public float Preco{get; set;}
        //caminho do diretorio
        private const string PAHT_DIRETORIO = "C:\\Users\\Phellipe\\Desktop\\senai\\c#\\crudRecriado\\Database"; //teste
        //local onde o aquivo vai ser criado
        private const string PAHT_ARQUIVO ="Database/Produto.csv";

        
        //construtor
        public Produtos(int a_codigo, string a_nome, float a_preco){
            this.Codigo = a_codigo;
            this.Nome = a_nome;
            this.Preco = a_preco;

            //verificação se o diretorio existe, caso não exista será criado
            if(!Directory.Exists(PAHT_DIRETORIO)){
                Directory.CreateDirectory(PAHT_DIRETORIO);
            }
            //fazendo o mesmo com o arquivo, verificando se ele existe, cado contrario, vai ser criado
            //.close apos criar o arquivo, ele fecha o arquivo criado
            if(!File.Exists(PAHT_ARQUIVO)){
                File.Create(PAHT_ARQUIVO).Close();
            }
        }
        //fim construtor
         
         /// <summary>
            /// Cria as separações dos dados no arquivo csv, escolha , ou ; para fazer a separação e registrar no arquivo certinho, esse metodo é interno.
         /// </summary>
         /// <returns>devolve a string já formatada, com todos os dados</returns>
         private string CriarLinha(Produtos c_produto){
             return $"{c_produto.Codigo};{c_produto.Nome};{c_produto.Preco}";
         }
         //fim do metodo

        /// <summary>
            /// Aqui começa o cadastro dos produtos, usando um array que nele é inserido o metodo CriarLinhas, para formatar os dados dentro da string.
        /// </summary>
         public void Cadastrar(Produtos c_produto){
             var linha = new string[]{CriarLinha()};
             //caminho do arquivo onde os dados formatados e cadastrados será salvo
             //O AppendAllLines precisa de um array, no array coloque a string com os dados formatados no metodo anterior
             File.AppendAllLines(PAHT_ARQUIVO,linha);
         }
          //fim do metodo
          
          /// <summary>
            /// Guarda as linhas do banco de dados no array. Depois, analisa as linhas do array e separa todos os dados em outro array. 
            ///Cria depois da analise um objeto com todos esses dados (um objeto pra cada registro do banco de dados) e  adiciona em uma lista de produtos. 
          /// </summary>
          /// <returns>retorna a lista criada lista.</returns>
          public List<Produtos> Ler(){
              //criando uma lista de produtos
              List<Produtos> produtos = new List<Produtos>();
              //grava as linhas do banco de dados 
              string[] linhas = File.ReadAllLines(PAHT_ARQUIVO);
              //analise de todas as linhas do banco de dados
              foreach(string a_linha in linhas){
                  //separação das dados de cada linha
                  string[] dados = a_linha.Split(";");
                  //cria um novo produto com os dados ja separados (objeto)
                  //convertanto dados em int e float
                  Produtos o_produto = new Produtos(Int32.Parse(dados[0]),dados[1], float.Parse(dados[2]));
                  //adicionando os itens na lista
                  produtos.Add(o_produto);
              }
              //testando a biblioteca LINQ
              //Organização em ordem alfabetica
              produtos = produtos.OrderBy(o_produto => o_produto.Nome).ToList();

              //retorna a lista
              return produtos;
          }
          //fim do metodo
        /// <summary>
        /// Sobrecarga no método Ler()  para achar uma instância com um nome especificado, usando um parametro.
        /// </summary>
        /// <param name="a_nome">nome para o "filtro"</param>
        /// <returns>lista com todos os objetos com o mesmo nome</returns>
        public List<Produtos> Ler(string a_nome){
            //lista alvo da verificação
            List<Produtos> produtos = new List<Produtos>();
            //verificação da lista
            foreach(Produtos produto in Ler() ){
                if(produto.Nome == a_nome){
                    produtos.Add(produto);
                }
            }
            //retorna a lista criada
            return produtos;
        }
        //fim do metodo


          /// <summary>
             /// Reescreve o csv a partir de um "backup".
        /// </summary>
        /// <param name="lines">Uma lista, que funciona como um backup.</param>
        private void Reescrever(List<string> r_linhas) {
            using( StreamWriter output = new StreamWriter(PAHT_ARQUIVO) ) {
                foreach(string ln in r_linhas) {
                    output.Write($"{ln}\n");
                }
            }
        }
        //fim do metodo
        /// <summary>
        /// remover rgistros do banco de dados atraves de uma palavra chave
        /// </summary>
        /// <param name="p_chave">palavras iguais as do filtro serão excluidas</param>
        public void Remover(string p_chave){
            //backup dos arquivos sempre é bom
            //lista que servirá como backup
            List<string> b_linhas = new List<string>();

            //testando o sTreamReader
            //streamread tem diretivas de using (using System.IO;) e serve para ler aquivo e fechalo em seguida
            //colocando o caminho do arquivo que vai ser lido
            using( StreamReader arquivo = new StreamReader(PAHT_ARQUIVO)){
                string s_linha;

                //vai procurar linhas vazias no arquivo, enquanto não achar, vai adicionar novas linhas no arquivo criado como backup
                while((s_linha = arquivo.ReadLine()) != null){
                    //add linjhas na lista de backup
                    b_linhas.Add(s_linha);
                }
                //remove as linhas com mesmo nome do filtro
                //pesquisar Lambida
                b_linhas.RemoveAll(l => l.Contains(p_chave));
            }
            //fim using
            //falta reescrever o aquivo cvs
            // testando posicionamento de comandos 
            //reescreve o cvg
            Reescrever(b_linhas);
        }
        //fim mtodo
        public void AlteraProduto(Produtos a_produtos){
            //Lista que servirá como um backup do csv.
            List<string> linhas = new List<string>();

            //StreamReader em  using serve para ler o arquivo e em seguida fechá-lo. mesma coisa
            using( StreamReader arquivo = new StreamReader(PAHT_ARQUIVO) ) { 
                string s_linha;

                //Vai adicionar a linha no "backup" até encontrar uma linha vazia.
                while( (s_linha = arquivo.ReadLine()) != null ) {
                    linhas.Add(s_linha);
                }
                //remove o produto do csv que tem o odigo igual ao do produto novo.
                linhas.RemoveAll(x => x.Split(";")[0].Contains( a_produtos.Codigo.ToString() ) );

                //adicona linhas novas
                linhas.Add(CriarLinha(a_produtos));

                //reescreve arquivo
                Reescrever(linhas);
        }
        //fim metodo
        
        
    }
}
}