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
using Main_Project;
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        private BookingReservation bookingReservation;
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
            var list = context.BookingDetails.Where(x => x.BookingReservationId == bookingReservation.BookingReservationId).Include(x => x.Room).ToList();
            BookingDetails.ItemsSource = list;
            txtId.ItemsSource = context.RoomInformations.ToList();
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
            if (txtId.SelectedItem!=null&&!string.IsNullOrWhiteSpace(dpEnd.Text)&& !string.IsNullOrWhiteSpace(dpStart.Text))
            { 
                DateOnly startDate = DateOnly.FromDateTime(dpStart.SelectedDate.Value);
                DateOnly endDate = DateOnly.FromDateTime(dpEnd.SelectedDate.Value);
                if(!input.isDateValid(startDate, endDate))
                {
                    MessageBox.Show("Start date must be less than end date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(input.isDateAfterToday(startDate))
                {
                    MessageBox.Show("End date must be less than today", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(input.isDateOverlap(startDate,endDate))
                {
                    MessageBox.Show("Cannot book this date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var room = txtId.SelectedItem as RoomInformation;
                var booking = context.BookingDetails.FirstOrDefault(x => x.Room==room&&x.BookingReservationId==bookingReservation.BookingReservationId);

                DateTime startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
                DateTime endDateTime = endDate.ToDateTime(new TimeOnly(0, 0));

                // Calculate the total days between the two DateTime values
                int totalDays = (endDateTime - startDateTime).Days;

                // Calculate the actualPrice using the totalDays and room.RoomPricePerDay
                decimal? actualPrice = totalDays * room.RoomPricePerDay;
                if (booking == null)
                {
                    var bookingDetail = new BookingDetail()
                    {
                        StartDate = startDate,
                        Room = room,
                        EndDate = endDate,
                        ActualPrice = actualPrice.GetValueOrDefault(),
                        BookingReservationId = bookingReservation.BookingReservationId,
                        BookingReservation = bookingReservation
                    };
                    txtPrice.Text = actualPrice.ToString();
                    context.BookingDetails.Add(bookingDetail);
                    context.SaveChanges();
                    Load();
                }
                else
                {
                    var result = MessageBox.Show("Do you want to update this booking?", "Update Booking", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        booking.Room = room;
                        booking.StartDate = startDate;
                        booking.EndDate = endDate;
                        booking.ActualPrice = actualPrice.GetValueOrDefault();
                        txtPrice.Text = actualPrice.ToString();
                        context.Update(booking);
                        context.SaveChanges();
                        Load();
                    }
                }

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
                var booking = context.BookingDetails.FirstOrDefault(x => x.Room == room && x.BookingReservationId == bookingReservation.BookingReservationId);
                if (booking != null)
                {
                    var result = MessageBox.Show("Do you want to delete this booking?", "Delete Booking", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        context.BookingDetails.Remove(booking);
                        context.SaveChanges();
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
