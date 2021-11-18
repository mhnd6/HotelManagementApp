using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;

namespace DataAcessLibrary.Data
{
    public interface IDataBaseData
    {
        void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int bookingId);
        List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        RoomTypeModel GetRoomTypeById(int id);
        List<BookingModel> SearchBookings(string lastName);
    }
}