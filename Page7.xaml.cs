using Question2;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public Page7()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page2(4));
            Load();
        }
        private void Load()
        {
            Reservation.ItemsSource = MainWindow.INSTANCE.context.BookingDetails.Include(x => x.Room).Include(x=> x.BookingReservation).Include(x => x.BookingReservation.Customer).OrderByDescending(x=> x.BookingReservationId).ToList();
        }

        private void DpStart_OnCalendarClosed(object sender, RoutedEventArgs e)
        {
            var start = dpStart.SelectedDate;
            var end = dpEnd.SelectedDate;
            if (start != null && end != null)
            {
                Reservation.ItemsSource = MainWindow.INSTANCE.context.BookingDetails
                    .Include(x => x.Room)
                    .Include(x => x.BookingReservation)
                    .Include(x => x.BookingReservation.Customer)
                    .Where(x => x.BookingReservation.BookingDate >= DateOnly.FromDateTime(start.GetValueOrDefault()) && x.BookingReservation.BookingDate <= DateOnly.FromDateTime(end.GetValueOrDefault()))
                    .ToList();
            }
        }

        private void ButtonBase_OnClickReset(object sender, RoutedEventArgs e)
        {
            dpEnd.SelectedDate = null;
            dpStart.SelectedDate = null;
            Load();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            // Cast the sender to a Hyperlink
            var hyperlink = sender as Hyperlink;
            if (hyperlink != null)
            {
                // Get the DataMainWindow.INSTANCE.context of the Hyperlink, which should be your data item
                var dataItem = hyperlink.DataContext;

                // Assuming your data item has a property named 'BookingReservationId'
                var propertyInfo = dataItem.GetType().GetProperty("BookingReservationId");
                if (propertyInfo != null)
                {
                    var bookingReservationId = propertyInfo.GetValue(dataItem, null);

                    var reservation = MainWindow.INSTANCE.context.BookingReservations.FirstOrDefault(x => x.BookingReservationId == (int)bookingReservationId);
                    if(reservation == null)
                    {
                        MessageBox.Show("Reservation not found");
                        return;
                    }
                    // Now that you have the BookingReservationId, you can use it to open Page6
                    // Assuming Page6 has a constructor that accepts a bookingReservationId
                    Window popupWindow = new Popup()
                    {
                        Title = "Pop up",
                        Content = new Page6(reservation), // Initialize Page6 with the bookingReservationId
                        SizeToContent = SizeToContent.WidthAndHeight,
                        ResizeMode = ResizeMode.CanResizeWithGrip
                    };
                    popupWindow.Show();
                }
            }

            // Prevent the default navigation behavior
            e.Handled = true;
        }

        private void Reservation_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SaveDataGridToCsvDialog(Reservation);
        }


        public static void SaveDataGridToCsv(DataGrid dataGrid, string filePath)
        {
            var sb = new StringBuilder();

            // Get headers
            var headers = dataGrid.Columns
                .Select(column => column.Header.ToString())
                .ToArray();
            sb.AppendLine(string.Join(",", headers));

            // Get rows
            foreach (var item in dataGrid.Items)
            {
                var row = item as dynamic;
                if (row == null) continue;

                var cells = dataGrid.Columns.Select(column =>
                {
                    var binding = (column as DataGridBoundColumn)?.Binding as System.Windows.Data.Binding;
                    var value = binding?.Path.Path.Split('.').Aggregate((object)row, (obj, property) => obj?.GetType().GetProperty(property)?.GetValue(obj, null));
                    return value?.ToString();
                }).ToArray();

                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        public static void SaveDataGridToCsvDialog(DataGrid dataGrid)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = ".csv"
            };

            if (dialog.ShowDialog() == true)
            {
                SaveDataGridToCsv(dataGrid, dialog.FileName);
            }
        }

    }
}
