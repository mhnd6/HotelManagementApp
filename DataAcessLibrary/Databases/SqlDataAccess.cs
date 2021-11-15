using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAcessLibrary.Databases
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration config;

        public SqlDataAccess(IConfiguration config)
        {
            this.config = config;
        }
        public List<T> LoadData<T, U>(string sqlStatment,
                                      U parameters,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            string connectionString = config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rooms = connection.Query<T>(sqlStatment, parameters, commandType: commandType).ToList();
                return rooms;
            }
        }
        public void SaveData<T>(string sqlStatment,
                                T parameters,
                                string connectionStringName,
                                bool isStoredProcedure = false)
        {
            string connectionString = config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatment, parameters, commandType: commandType);
            }
        }
    }
}
