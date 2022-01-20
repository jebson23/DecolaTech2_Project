using System.Collections.Generic;
using System.Data;
using Dapper;
using DIO.Series.Data;
using DIO.Series.Data.Queries;
using DIO.Series.Interfaces;

namespace DIO.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private static DataBaseContext DbConnection = new DataBaseContext();
        public void Atualizar(Serie entidade)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", entidade.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Genero", entidade.Genero, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Titulo", entidade.Titulo, DbType.String, ParameterDirection.Input);
            parameters.Add("@Descricao", entidade.Descricao, DbType.String, ParameterDirection.Input);
            parameters.Add("@Ano", entidade.Ano, DbType.Int32, ParameterDirection.Input);

            DbConnection.Update(SeriesQuerySql.UpdateSeries, parameters);
        }

        public void Exclui(int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

            DbConnection.Delete(SeriesQuerySql.DeleteSeries, parameters);
        }

        public Serie Insere(Serie entidade)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Genero", entidade.Genero, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Titulo", entidade.Titulo, DbType.String, ParameterDirection.Input);
            parameters.Add("@Descricao", entidade.Descricao, DbType.String, ParameterDirection.Input);
            parameters.Add("@Ano", entidade.Ano, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Id",ParameterDirection.Output);


            entidade.Id = DbConnection.Insert<int>(SeriesQuerySql.InsertSeries, parameters,"@Id");
            return entidade;
        }

        public List<Serie> Listar()
        {
            return DbConnection.Get<Serie>(sql: SeriesQuerySql.FindSeries);
        }
 
        public Serie RetornaPorId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

            return  DbConnection.Find<Serie>(SeriesQuerySql.FindSeriesId, parameters);
        }
    }
}
