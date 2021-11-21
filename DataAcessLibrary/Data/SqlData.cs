using DataAcessLibrary.Databases;
using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcessLibrary.Data
{
    public class SqlData : IDataBaseData
    {
        private readonly ISqlDataAccess db;
        private const string connectionStringName = "SqlDB";

        public SqlData(ISqlDataAccess db)
        {
            // The ISqlDataAccess will be through dependcy injection
            this.db = db;
        }
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                 new { startDate, endDate },
                 connectionStringName,
                 true);
        }

        public void BookGuest(string firstName,
                              string lastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomTypeId)
        {
            GuestModel guest = db.LoadData<GuestModel, dynamic>("dbo.spGuest_Insert",
                                                                new { firstName, lastName },
                                                                connectionStringName,
                                                                true).First();

            RoomTypeModel roomType = db.LoadData<RoomTypeModel, dynamic>(
                "select * from dbo.RoomTypes where Id = @Id",
                new { Id = roomTypeId },
                connectionStringName,
                false).First();

            TimeSpan timeStying = endDate.Date.Subtract(startDate.Date);

            List<RoomModel> availableRooms = db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                             new { startDate, endDate, roomTypeId },
                                                                             connectionStringName,
                                                                             true);

            db.SaveData("dbo.spBookings_insert",
                                      new
                                      {
                                          roomId = availableRooms.First().Id,
                                          guestId = guest.Id,
                                          startDate = startDate,
                                          endDate = endDate,
                                          totalCost = timeStying.Days * roomType.Price
                                      },
                                      connectionStringName,
                                      true);
        }

        public List<BookingModel> SearchBookings(string lastName)
        {
            return db.LoadData<BookingModel, dynamic>("dbo.spBookings_Search",
                                               new
                                               {
                                                   lastName,
                                                   startDate = DateTime.Now.Date
                                               },
                                               connectionStringName,
                                               true);
        }

        public void CheckInGuest(int bookingId)
        {
            db.SaveData("spBookings_CheckedIn",
                new { Id = bookingId },
                connectionStringName,
                true);

        }

        public RoomTypeModel GetRoomTypeById(int id)
        {
            return db.LoadData<RoomTypeModel, dynamic>(
                "dbo.spRoomTypes_GetById",
                new { id },
                connectionStringName,
                true).FirstOrDefault();
        }


    }
}
