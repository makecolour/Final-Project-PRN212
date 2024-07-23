using Question2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        
        public Page4()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page2(2));
            Load();
        }

        private void Load()
        {
            var list = MainWindow.INSTANCE.context.RoomInformations.OrderByDescending(x => x.RoomStatus).Include(x=>x.RoomType).ToList();
            Rooms.ItemsSource = list;
            cbRoomType.ItemsSource = MainWindow.INSTANCE.context.RoomTypes.ToList();
            cbRoomType.DisplayMemberPath = "RoomTypeName";
        }

        private void Rooms_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as DataGrid).SelectedItem;
            if (item != null)
            {
                var customer = item as RoomInformation;
                cbRoomType.SelectedItem = customer.RoomType;
                txtRoomType.Text = customer.RoomType.TypeDescription;
                var status = customer.RoomStatus;
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
            txtId.Text = "";
           txtPrice.Text = "";
           txtNumber.Text = "";
           txtPrice.Text = "";
           txtMaxCapacity.Text = "";
           txtRoomType.Text = "";
           deactive.IsChecked = false;
           active.IsChecked = false;
           cbRoomType.SelectedValue = null;
           txtDescription.Text = null;
        }

        private void CbRoomType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (cbRoomType.SelectedItem is RoomType selectedItem)
           {
               txtRoomType.Text = selectedItem.TypeDescription;
           }
           else
           {
               txtRoomType.Text = string.Empty; // Clear the TextBox if no item is selected
           }
        }

        private void ButtonBase_OnClickAdd(object sender, RoutedEventArgs e)
        {
            var listNumber = MainWindow.INSTANCE.context.RoomInformations.Where(x => x.RoomNumber == txtNumber.Text).ToList();
            if (string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(txtMaxCapacity.Text) || cbRoomType.SelectedItem == null || !Regex.IsMatch(txtMaxCapacity.Text, @"^\d+$"))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var list = MainWindow.INSTANCE.context.RoomInformations.Where(x => x.RoomId == id).ToList();
                if (listNumber.Count > 0)
                {
                    MessageBox.Show("Room number already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (list.Count <= 0)
                {
                    
                    var room = new RoomInformation()
                    {
                        RoomNumber = txtNumber.Text,
                        RoomDetailDescription = txtDescription.Text,
                        RoomMaxCapacity = int.Parse(txtMaxCapacity.Text),
                        RoomTypeId = (cbRoomType.SelectedItem as RoomType).RoomTypeId,
                        RoomStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                        RoomPricePerDay = decimal.Parse(txtPrice.Text),
                        RoomType = cbRoomType.SelectedItem as RoomType
                    };
                    MainWindow.INSTANCE.context.RoomInformations.Add(room);
                    MainWindow.INSTANCE.context.SaveChanges();
                    Load();
                }
                else
                {
                    if(listNumber.Count>0 || !string.IsNullOrWhiteSpace(txtNumber.Text))
                    {
                        MessageBox.Show("Room number already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var result = MessageBox.Show("Do you want to update this room?", "Update Room", MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                    {
                        var room = list.FirstOrDefault();
                        room.RoomNumber = txtNumber.Text;
                        room.RoomDetailDescription = txtDescription.Text;
                        room.RoomMaxCapacity = int.Parse(txtMaxCapacity.Text);
                        room.RoomTypeId = (cbRoomType.SelectedItem as RoomType).RoomTypeId;
                        room.RoomStatus = active.IsChecked == true ? (byte)1 : (byte)0;
                        room.RoomPricePerDay = decimal.Parse(txtPrice.Text); ;
                        MainWindow.INSTANCE.context.RoomInformations.Update(room);
                        MainWindow.INSTANCE.context.SaveChanges();
                        Load();
                    }
                }
            }
            else
            {
                var room = new RoomInformation()
                {
                    RoomNumber = txtNumber.Text,
                    RoomDetailDescription = txtDescription.Text,
                    RoomMaxCapacity = int.Parse(txtMaxCapacity.Text),
                    RoomTypeId = (cbRoomType.SelectedItem as RoomType).RoomTypeId,
                    RoomStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                    RoomPricePerDay = decimal.Parse(txtPrice.Text),
                    RoomType = cbRoomType.SelectedItem as RoomType
                };
                MainWindow.INSTANCE.context.RoomInformations.Add(room);
                MainWindow.INSTANCE.context.SaveChanges();
                Load();
            }
        }
        private void ButtonBase_OnClickDelete(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var list = MainWindow.INSTANCE.context.RoomInformations.Where(x => x.RoomId == id).ToList();
                if (list.Count > 0)
                {
                    var result = MessageBox.Show("Do you want to delete this room?", "Delete Room", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        var room = list.FirstOrDefault();
                        var booking = MainWindow.INSTANCE.context.BookingDetails.Where(x => x.RoomId == room.RoomId).ToList();
                        if (booking.Count==0)
                        {
                            MainWindow.INSTANCE.context.RoomInformations.Remove(room);
                        }
                        else
                        {
                            room.RoomStatus = 0;
                            MainWindow.INSTANCE.context.RoomInformations.Update(room);
                        }

                        MainWindow.INSTANCE.context.SaveChanges();
                        Load();
                    }
                }
                else
                {
                    MessageBox.Show("Room not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
