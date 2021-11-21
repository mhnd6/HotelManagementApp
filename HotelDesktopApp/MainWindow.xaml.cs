using DataAcessLibrary.Data;
using DataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private readonly IDataBaseData db;
        List<BookingModel> bookings = new List<BookingModel>();

        public MainWindow(IDataBaseData db)
        {
            InitializeComponent();
            this.db = db;
            
        }

        private void searchBookings_Click(object sender, RoutedEventArgs e)
        {
            bookings = db.SearchBookings(lastNameText.Text);
            bookingsList.ItemsSource = bookings;
        }

        private void checkIn_Click(object sender, RoutedEventArgs e)
        {
            var checkInForm = App.serviceProvider.GetService<CheckIn>();
            var model = (BookingModel)((Button)e.Source).DataContext;

            checkInForm.PopulateCheckInInfo(model);

            checkInForm.Show();

        }

    }
}
