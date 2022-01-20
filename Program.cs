using System;
using DIO.Series.Data;
using DIO.Series.Data.Queries;

namespace DIO.Series
{
    public class Program 
    {
        private static DataBaseContext DbConnection = new DataBaseContext();
        static SerieRepositorio repositorio = new SerieRepositorio();
        public static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Program.ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        LimparTela();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços!!!");
            Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Lista de Séries:");
            var lista = repositorio.Listar(); //repositorio.Lista();

            if (lista.Count() == 0) {Console.WriteLine("Não há series cadastradas."); }

            foreach (var serie in lista)
            {
                Console.WriteLine("\n" + "#ID {0}: {1}  {2}", serie.retornaId(), serie.retornaTitulo(), serie.DeletedAt.HasValue ? "*Excluido*" : "");
            }

        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova Série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("Escolha o Gênero entre as opções acima: ");
            int entradaGenero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Digite o Ano de Início da Série: ");
            int entradaAno = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine() ?? string.Empty;

        
            Serie novaSerie = repositorio.Insere(new Serie {
                Genero= (Genero)entradaGenero,
                Titulo= entradaTitulo,
                Descricao= entradaDescricao,
                Ano = entradaAno
            });
            
            Console.WriteLine("\n" + "Série inserida com sucesso: " + "\n");
            Console.WriteLine(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Informe o ID da Série: ");
            int id = Convert.ToInt32(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("Escolha o Gênero entre as opções acima: ");
            int entradaGenero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Digite o Ano de Início da Série: ");
            int entradaAno = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine() ?? string.Empty;

            Serie atualizarSerie = new Serie {
                Id = id,
                Genero = (Genero)entradaGenero,
                Titulo= entradaTitulo,
                Descricao= entradaDescricao,
                Ano= entradaAno
            };

            repositorio.Atualizar(atualizarSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Informe o ID da Série: ");
            int id = Convert.ToInt32(Console.ReadLine());

            repositorio.Exclui(id);
        }

        private static void VisualizarSerie()
        {
            Console.Write("Informe o ID da Série: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var serie = repositorio.RetornaPorId(id);

            Console.WriteLine("\n" + serie);
        }

        private static void LimparTela()
        {
            Console.Clear();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu Dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar Séries");
            Console.WriteLine("2- Inserir nova Série");
            Console.WriteLine("3- Atualizar Série");
            Console.WriteLine("4- Excluir Série");
            Console.WriteLine("5- Visualizar Série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = (Console.ReadLine() ?? string.Empty).ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }

}
