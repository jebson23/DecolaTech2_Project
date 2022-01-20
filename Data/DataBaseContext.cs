using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace DIO.Series.Data
{
    public class DataBaseContext
    {
        private string ConnectionString = "";
        private IDbTransaction? transaction;
        private IDbConnection? connection;

        public DataBaseContext()
        {
            // this.connection = new MySqlConnection(this.ConnectionString);
        }
        public DataBaseContext(string connectionString) : this()
        {
            this.ConnectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        private void OpenConnection()
        {
            if(this.connection == null) this.connection = this.CreateConnection();

            if(this.connection.State != ConnectionState.Open) this.connection.Open();

            this.transaction = this.connection.BeginTransaction();
        }

        private void CloseConnection()
        {
            if (this.connection == null) return;

            if (this.connection.State != ConnectionState.Closed) this.connection.Close();

            this.transaction = null;
        }

        private void Commit()
        {
            if (this.transaction == null) return;

            if (this.connection?.State != ConnectionState.Open) return;

            this.transaction.Commit();
        }

        private void RollBack()
        {
            if (this.transaction == null) return;

            if (this.connection?.State != ConnectionState.Open) return;

            this.transaction.Rollback();
        }

        public T? Find<T>(string sql, DynamicParameters parameters, CommandType commandType)
        {
            this.OpenConnection();
            T? result = this.connection.Query<T>(sql: sql, param: parameters, transaction: this.transaction, commandTimeout: 20000, commandType: commandType).FirstOrDefault<T>();
            this.CloseConnection();
            return result;
        }

        public T? Find<T>(string sql, DynamicParameters parameters)
        { return this.Find<T>(sql: sql, parameters: parameters, commandType: CommandType.Text); }

        
        public T? Find<T>(string sql)
        { return this.Find<T>(sql: sql, parameters: new DynamicParameters()); }

        public List<T> Get<T>(string sql, DynamicParameters parameters, CommandType commandType)
        {
            this.OpenConnection();
            List<T> result = this.connection.Query<T>(sql: sql, param: parameters, transaction: this.transaction, commandTimeout: 20000, commandType: commandType).ToList<T>();
            this.CloseConnection();
            return result;
        }

        public List<T> Get<T>(string sql, DynamicParameters parameters)
        { return this.Get<T>(sql: sql, parameters: parameters, commandType: CommandType.Text); }

        
        public List<T> Get<T>(string sql)
        { return this.Get<T>(sql: sql, parameters: new DynamicParameters()); }

        public T Insert<T>(string sql, DynamicParameters parameters, string output)
        {
            this.OpenConnection();

            this.connection.Execute(sql, parameters);

            this.Commit();

            this.CloseConnection();

            return parameters.Get<T>(output);
        }

        public void Update(string sql, DynamicParameters parameters)
        {
            this.OpenConnection();

            this.connection.Execute(sql, parameters);

            this.Commit();

            this.CloseConnection();
        }

        public void Delete(string sql, DynamicParameters parameters)
        {
            this.Update(sql, parameters);
        }
    }
}
