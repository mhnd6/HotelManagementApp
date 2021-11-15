using DataAcessLibrary.Databases;
using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess db;
        private const string connectionStringName = "SqlDB";

        public SqlData(ISqlDataAccess db)
        {
            this.db = db;
        }
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
           return db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                new { startDate, endDate},
                connectionStringName,
                true);
        }
    }
}
