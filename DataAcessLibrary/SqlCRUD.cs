using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLibrary
{
    public class SqlCRUD
    {
        private readonly string connectionString;
        private SqlDataAccess db = new SqlDataAccess();
        public SqlCRUD(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateRooms(List<RoomModel> rooms)
        {
            string sql = "insert into dbo.Rooms (RoomNumber, RoomTypeId) values (@RoomNumber, @RoomTypeId);";

            foreach (var r in rooms)
            {
                db.SaveData(sql, new
                {
                    r.RoomNumber,
                    r.RoomTypeId
                }, connectionString);
            }
        }

        public void CreateRoomTypes(List<RoomTypeModel> roomTypes)
        {
            string sql = "insert into dbo.RoomTypes (Title, Description, Price) values (@Title, @Description, @Price);";

            foreach (var rt in roomTypes)
            {
                db.SaveData(sql, new
                {
                    rt.Title,
                    rt.Description,
                    rt.Price
                }, connectionString);
            }
        }
    }
}
