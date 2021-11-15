using System.Collections.Generic;

namespace DataAcessLibrary.Databases
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatment,
                               U parameters,
                               string connectionStringName,
                                bool isStoredProcedure = false);
        void SaveData<T>(string sqlStatment,
                         T parameters,
                         string connectionStringName,
                          bool isStoredProcedure = false);
    }
}