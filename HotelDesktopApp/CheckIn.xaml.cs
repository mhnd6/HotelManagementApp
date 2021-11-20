using DataAcessLibrary.Data;
using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for CheckIn.xaml
    /// </summary>
    public partial class CheckIn : Window
    {
        private readonly IDataBaseData db;
        private BookingModel _data = null;

        public CheckIn(IDataBaseData db)
        {
            InitializeComponent();
            this.db = db;
        }

        public void PopulateCheckInInfo(BookingModel data)
        {
            _data = data;
            firstNameText.Text = data.FirstName;
            lastNameText.Text = data.LastName;
            titleText.Text = data.Title;
            roomNumberText.Text = data.RoomNumber;
            totalCostText.Text = String.Format("{0:C}", data.TotalCost);
        }

        private void checkInGuest_Click(object sender, RoutedEventArgs e)
        {
            db.CheckInGuest(_data.Id);
            this.Close();
        }
    }
}
