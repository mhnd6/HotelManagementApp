using DataAcessLibrary.Data;
using DataAcessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace HotelWebApp.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDataBaseData db;

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        public RoomTypeModel RoomType { get; set; }

        public BookRoomModel(IDataBaseData db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                RoomType = db.GetRoomTypeById(RoomTypeId);
            }
        }

        public IActionResult OnPost()
        {
            db.BookGuest(FirstName, LastName, StartDate, EndDate, RoomTypeId);
            return RedirectToPage("/Index");
        }
    }
}
