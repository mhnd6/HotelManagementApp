using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAcessLibrary
{
    public class SqlDataAccess
    {
        public void SaveData<T>(string sqlStatment, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatment, parameters);
            }
        }
    }
}
