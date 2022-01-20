namespace DIO.Series
{
    public class Serie : EntidadeBase
    {
        public Genero Genero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Ano { get; set;}
        public DateTime? DeletedAt { get; set; }

        public static implicit operator string(Serie s){
            string retorno = "";
            retorno += "Gênero: " + s.Genero + Environment.NewLine;
            retorno += "Título: " + s.Titulo + Environment.NewLine;
            retorno += "Descrição: " + s.Descricao + Environment.NewLine;
            retorno += "Ano de Início: " + s.Ano + Environment.NewLine;
            retorno += "Excluído: " + ((s.DeletedAt.HasValue) ? "Sim" : "Não");
            return retorno;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public int retornaId()
        {
            return this.Id;
        }
    }
}