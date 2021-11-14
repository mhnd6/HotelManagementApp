using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLibrary
{
    public static class StarterData
    {
        public static List<RoomTypeModel> GetRoomTypes()
        {
                List<RoomTypeModel> output = new List<RoomTypeModel>
            {
                new RoomTypeModel
                {
                    Title = "Queen",
                    Description = "Very large room",
                    Price = 200
                }
            };
            return output;
        }

        public static List<RoomModel> GetRooms()
        {
            List<RoomModel> output = new List<RoomModel>
            {
                new RoomModel
                {
                    RoomNumber = "101",
                    RoomTypeId = 1
                }
            };

            return output;
        }
    }
}
