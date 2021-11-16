using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLibrary.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool CheckIn { get; set; }

    }
}
