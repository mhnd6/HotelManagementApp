using DataAcessLibrary.Data;
using DataAcessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Pages
{
    public class RoomSearchModel : PageModel
    {
        private readonly IDataBaseData db;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        public List<RoomTypeModel> AvailableRoomTypes { get; set; }

        public RoomSearchModel(IDataBaseData db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            if (SearchEnabled)
            {
              AvailableRoomTypes = db.GetAvailableRoomTypes(StartDate, EndDate);
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new {SearchEnabled = true, StartDate, EndDate});
        }
    }
}
