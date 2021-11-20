using System.Collections.Generic;

namespace DataAcessLibrary.Databases
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatment, U parameters, string connectionStringName);
        void SaveData<T>(string sqlStatment, T parameters, string connectionStringName);
    }
}