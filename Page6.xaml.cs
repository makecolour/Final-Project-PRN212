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
using Main_Project;
using Main_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {

        private readonly BookingReservation bookingReservation;
        private Input input = new Input();
        public Page6(BookingReservation bookingReservation)
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
            txtId.ItemsSource = MainWindow.INSTANCE.context.RoomInformations.ToList();
            txtId.DisplayMemberPath = "RoomNumber";
        }
        private void Rooms_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem;
            if (item != null)
            {
                var customer = item as BookingDetail;
                txtId.SelectedItem = customer.Room;
                txtPrice.Text = customer.ActualPrice.ToString();
                dpStart.SelectedDate = DateTime.Parse(customer.StartDate.ToString());
                dpEnd.SelectedDate = DateTime.Parse(customer.EndDate.ToString());

            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            txtId.SelectedItem = null;
            txtPrice.Text = "";
            dpStart.SelectedDate = null;
            dpEnd.SelectedDate = null;
        }


        private void ButtonBase_OnClickAdd(object sender, RoutedEventArgs e)
        {
            if (txtId.SelectedItem!=null&&dpStart.SelectedDate!=null&&dpEnd.SelectedDate!=null)
            { 
                
                DateOnly startDate = DateOnly.FromDateTime(dpStart.SelectedDate.GetValueOrDefault());
                DateOnly endDate = DateOnly.FromDateTime(dpEnd.SelectedDate.GetValueOrDefault());
                var room = txtId.SelectedItem as RoomInformation;
                if (!input.isDateValid(startDate, endDate))
                {
                    return;
                }
                else if(!input.isDateAfterToday(startDate))
                {
                    return;
                }

                DateTime startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
                DateTime endDateTime = endDate.ToDateTime(new TimeOnly(0, 0));

                // Calculate the total days between the two DateTime values
                int totalDays = (endDateTime - startDateTime).Days;

                // Calculate the actualPrice using the totalDays and room.RoomPricePerDay
                decimal? actualPrice = totalDays * room.RoomPricePerDay;
                var bookingDetail = new BookingDetail()
                {
                    StartDate = startDate,
                    Room = room,
                    EndDate = endDate,
                    ActualPrice = actualPrice.GetValueOrDefault(),
                    BookingReservationId = bookingReservation.BookingReservationId,
                    BookingReservation = bookingReservation
                };
                if (input.isDateOverlap(bookingDetail))
                {
                    return;
                }
                txtPrice.Text = actualPrice.ToString();
                MainWindow.INSTANCE.context.BookingDetails.Add(bookingDetail);
                MainWindow.INSTANCE.context.SaveChanges();
                Load();

            }
            else
            {
                MessageBox.Show("Please fill all the blank", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (txtId.SelectedItem != null)
            {
                var room = txtId.SelectedItem as RoomInformation;
                var booking = MainWindow.INSTANCE.context.BookingDetails.FirstOrDefault(x => x.Room == room && x.BookingReservationId == bookingReservation.BookingReservationId);
                if (booking != null)
                {
                    var result = MessageBox.Show("Do you want to delete this booking?", "Delete Booking", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.INSTANCE.context.BookingDetails.Remove(booking);
                        MainWindow.INSTANCE.context.SaveChanges();
                        Load();
                    }
                }
                else
                {
                    MessageBox.Show("Booking not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
