using Microsoft.EntityFrameworkCore;
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
using static System.DateTime;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        public Page5()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page2(3));
            Load();
        }

        private void Load()
        {
            List<BookingReservation> list = context.BookingReservations.OrderByDescending(x => x.BookingStatus).Include(x => x.Customer).ToList();
            foreach (var reservation in list)
            {
                reservation.TotalPrice = 0;
                reservation.BookingDetails = context.BookingDetails.Where(x => x.BookingReservationId == reservation.BookingReservationId).Include(x => x.Room).ToList();
                foreach (var VARIABLE in reservation.BookingDetails)
                {
                    reservation.TotalPrice += VARIABLE.ActualPrice;
                }
                context.Update(reservation);
                context.SaveChanges();
            }
            Reservation.ItemsSource = list;
            cbCustomer.ItemsSource = context.Customers.ToList();
            cbCustomer.DisplayMemberPath = "CustomerFullName";
        }


        private void Rooms_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem;
            if (item != null)
            {
                var customer = item as BookingReservation;
                txtPrice.Text = customer.TotalPrice.ToString();
                txtId.Text = customer.BookingReservationId.ToString();
                cbCustomer.SelectedItem = customer.Customer;
                dpBooking.SelectedDate = Parse(customer.BookingDate.ToString());
                var status = customer.BookingStatus;
                if (status == 1)
                {
                    deactive.IsChecked = false;
                    active.IsChecked = true;
                }
                else
                {
                    deactive.IsChecked = true;
                    active.IsChecked = false;
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            txtPrice.Text = "";
            txtId.Text = "";
            dpBooking.SelectedDate = null;
            cbCustomer.SelectedItem = null;
            deactive.IsChecked = false;
            active.IsChecked = false;
        }

        private void ButtonBase_OnClickAdd(object sender, RoutedEventArgs e)
        {
            var lastBookingId = context.BookingReservations.OrderBy(r => r.BookingReservationId).LastOrDefault()?.BookingReservationId ?? 0;
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var reservation = context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == id);
                if(reservation == null)
                {
                    var booking = new BookingReservation()
                    {
                        BookingReservationId = lastBookingId + 1,
                        BookingDate = DateOnly.FromDateTime(Today),
                        Customer = cbCustomer.SelectedItem as Customer,
                        BookingStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                        BookingDetails = new List<BookingDetail>(),
                        CustomerId = cbCustomer.SelectedItem as Customer != null ? (cbCustomer.SelectedItem as Customer).CustomerId : 0
                    };
                    context.BookingReservations.Add(booking);
                    context.SaveChanges();
                    Load();
                }
                else
                {
                    var result = MessageBox.Show("Do you want to update this booking reservation?", "Update Booking BookingDetails", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        reservation.BookingReservationId = lastBookingId+1;
                        reservation.BookingStatus = active.IsChecked == true ? (byte)1 : (byte)0;
                        reservation.Customer = cbCustomer.SelectedItem as Customer;
                        reservation.BookingDate = DateOnly.FromDateTime(dpBooking.SelectedDate.GetValueOrDefault());
                        reservation.CustomerId = cbCustomer.SelectedItem != null && (Customer)cbCustomer.SelectedItem != null ? (cbCustomer.SelectedItem as Customer).CustomerId : 0;
                        context.Update(reservation);
                        context.SaveChanges();
                        Load();
                    }
                }
            }
            else
            {
                var booking = new BookingReservation()
                {
                    BookingReservationId = lastBookingId + 1,
                    BookingDate = DateOnly.FromDateTime(Today),
                    Customer = cbCustomer.SelectedItem as Customer,
                    BookingStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                    CustomerId = cbCustomer.SelectedItem as Customer != null ? (cbCustomer.SelectedItem as Customer).CustomerId : 0
                };
                context.Add(booking);
                context.SaveChanges();
                Load();
            }
        }

        private void ButtonBase_OnClickDelete(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var reservation = context.BookingReservations.Find(id);
                if(reservation != null)
                {
                    var result = MessageBox.Show("Do you want to delete this booking reservation?", "Delete Booking BookingDetails", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        reservation.BookingStatus = 0;
                        context.Update(reservation);
                        context.SaveChanges();
                        Load();
                    }
                }
                else
                {
                    MessageBox.Show("Booking reservation not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var reservation = context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == id);
                if(reservation != null)
                {
                    //MainWindow.INSTANCE.MainFrame.Navigate(new Page6(reservation));
                    Window popupWindow = new Window
                    {
                        Title = "Pop up",
                        Content = new Page6(reservation), // Set the content to Page6
                        SizeToContent = SizeToContent.WidthAndHeight, // Size window to content
                        ResizeMode = ResizeMode.CanResizeWithGrip // Optional: Prevent resizing
                    };
                    popupWindow.Show();
                    popupWindow.Closed += (sender, e) => Load();
                }
                else
                {
                    MessageBox.Show("Booking reservation not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
