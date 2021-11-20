using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DataAcessLibrary.Databases
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly IConfiguration config;

        public SqliteDataAccess(IConfiguration config)
        {
            this.config = config;
        }
        public List<T> LoadData<T, U>(string sqlStatment,
                                      U parameters,
                                      string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> rooms = connection.Query<T>(sqlStatment, parameters).ToList();
                return rooms;
            }
        }

        public void SaveData<T>(string sqlStatment,
                                T parameters,
                                string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatment, parameters);
            }
        }
    }
}
