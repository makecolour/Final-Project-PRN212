using Question2;
using System;
using System.Collections.Generic;
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
using Main_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page10.xaml
    /// </summary>
    public partial class Page10 : Page
    {
        private BookingReservation bookingReservation;
        public Page10(BookingReservation bookingReservation)
        {
            InitializeComponent();
            this.bookingReservation = bookingReservation;
            Load();
        }
        private void Load()
        {
            ContentControl.Content = "Reservation ID: " + bookingReservation.BookingReservationId;
            var list = MainWindow.INSTANCE.context.BookingDetails.Where(x => x.BookingReservationId == bookingReservation.BookingReservationId).Include(x => x.Room).ToList();
            BookingDetails.ItemsSource = list;
        }
    }
}
