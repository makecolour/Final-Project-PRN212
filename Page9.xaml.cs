using Question2.Models;
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
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page9.xaml
    /// </summary>
    public partial class Page9 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;

        public Page9()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page8(1));
            Load();
        }

        private void Load()
        {
            var list = context.BookingReservations.Where(x=> x.Customer.CustomerId==MainWindow.INSTANCE._customer.CustomerId).Include(x => x.Customer).ToList();
            Reservation.ItemsSource = list;
        }

        private void Reservation_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem;
            if (item != null)
            {
                var reservation = item as BookingReservation;
                Window popupWindow = new Window
                {
                    Title = "Pop up",
                    Content = new Page10(reservation), // Set the content to Page6
                    SizeToContent = SizeToContent.WidthAndHeight, // Size window to content
                    ResizeMode = ResizeMode.CanResizeWithGrip // Optional: Prevent resizing
                };
                popupWindow.Show();
                popupWindow.Closed += (sender, e) => Load();
            }
        }
    }
}
