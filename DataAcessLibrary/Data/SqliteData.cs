using DataAcessLibrary.Databases;
using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAcessLibrary.Data
{
    public class SqliteData : IDataBaseData
    {
        private readonly ISqliteDataAccess db;
        private const string connectionStringName = "SqliteDB";

        public SqliteData(ISqliteDataAccess db)
        {
            this.db = db;
        }

        public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            string sql = @"select 1 from Guests where FirstName = @firstName and LastName = @lastName";
            int results = db.LoadData<dynamic, dynamic>(sql,
                new { firstName, lastName }, connectionStringName).Count();

            if (results == 0)
            {
                sql = @"insert into Guests (FirstName, LastName)
		                                values (@firstName, @lastName)";
                db.SaveData(sql, new { firstName, lastName }, connectionStringName);
            }

            sql = @" select [Id], [FirstName], [LastName] 
	                                from Guests
	                                where FirstName = @firstName and LastName = @lastName LIMIT 1;";

            GuestModel guest = db.LoadData<GuestModel, dynamic>(sql,
                                                                new { firstName, lastName },
                                                                connectionStringName).First();

            RoomTypeModel roomType = db.LoadData<RoomTypeModel, dynamic>(
                "select * from RoomTypes where Id = @Id",
                new { Id = roomTypeId },
                connectionStringName).First();

            TimeSpan timeStying = endDate.Date.Subtract(startDate.Date);

            string sqlGetAvilableRooms = @"select r.*
	                                    from Rooms r
	                                    inner join RoomTypes t on t.Id = r.RoomTypeId
	                                    where r.RoomTypeId = @roomTypeId 
	                                    and r.Id not in (
	                                    select b.RoomId
	                                    from Bookings b
	                                    where (@startDate < b.StartDate and @endDate > b.EndDate)
		                                    or (b.StartDate <= @endDate and @endDate < b.EndDate)
		                                    or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                                    );";

            List<RoomModel> availableRooms = db.LoadData<RoomModel, dynamic>(sqlGetAvilableRooms,
                                                                             new { startDate, endDate, roomTypeId },
                                                                             connectionStringName);

            string sqlInsertBooking = @"insert into Bookings (RoomId, GuestId, StartDate, EndDate, TotalCost)
	                                  values (@roomId, @guestId, @startDate, @endDate, @totalCost)";
           
            db.SaveData(sqlInsertBooking,
                                      new
                                      {
                                          roomId = availableRooms.First().Id,
                                          guestId = guest.Id,
                                          startDate = startDate,
                                          endDate = endDate,
                                          totalCost = timeStying.Days * roomType.Price 
                                      },
                                      connectionStringName);
        }

        public void CheckInGuest(int bookingId)
        {
            string sql = @"update Bookings
	                    SET CheckIn = 1
	                    where Id = @Id;";

            db.SaveData(sql,
                new { Id = bookingId },
                connectionStringName);
        }

        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            string sql = @"select t.Id, t.Title, t.Description, t.Price
	                    from Rooms r
	                    inner join RoomTypes t on t.Id = r.RoomTypeId
	                    where r.Id not in (
	                    select b.RoomId
	                    from Bookings b
	                    where (@startDate < b.StartDate and @endDate > b.EndDate)
		                    or (b.StartDate <= @endDate and @endDate < b.EndDate)
		                    or (b.StartDate <= @startDate and @startDate < b.EndDate)
	                    )
	                    group by t.Id, t.Title, t.Description, t.Price";

            var output = db.LoadData<RoomTypeModel, dynamic>(sql,
                 new { startDate, endDate },
                 connectionStringName);

            output.ForEach(x => x.Price = x.Price / 100);

            return output;
        }

        public RoomTypeModel GetRoomTypeById(int id)
        {

            string sqlGetRoomTypeById = @"select [Id], [Title], [Description], [Price]
	                                    from RoomTypes
	                                    where Id = @id;";

            return db.LoadData<RoomTypeModel, dynamic>(
                sqlGetRoomTypeById,
                new { id },
                connectionStringName).FirstOrDefault();
        }

        public List<BookingModel> SearchBookings(string lastName)
        {
            string sql = @"select [b].[Id], [b].[RoomId], [b].[StartDate], [b].[EndDate], [b].[GuestId], 
	                    [b].[TotalCost], [b].[CheckIn], 
	                    [g].[FirstName], [g].[LastName], 
	                    [r].[RoomNumber], [r].[RoomTypeId], 
	                    [rt].[Title], [rt].[Description], [rt].[Price]
	                    from Bookings b
	                    inner join Guests g on b.GuestId = g.Id
	                    inner join Rooms r on b.RoomId = r.Id
	                    inner join RoomTypes rt on r.RoomTypeId = rt.Id
	                    where b.StartDate = @startDate and g.LastName = @lastName";

            var output = db.LoadData<BookingModel, dynamic>(sql,
                                               new
                                               {
                                                   lastName,
                                                   startDate = DateTime.Now.Date
                                               },
                                               connectionStringName);

            output.ForEach(x =>
            {
                x.TotalCost = x.TotalCost / 100;
                x.Price = x.Price / 100;
            });

            return output;
        }
    }
}
