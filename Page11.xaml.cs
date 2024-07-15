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

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page11.xaml
    /// </summary>
    public partial class Page11 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        private Customer customer = MainWindow.INSTANCE._customer;
        public Page11()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page8(2));
            Load();
        }

        private void Load()
        {
            txtId.Text = customer.CustomerId.ToString();
            txtName.Text = customer.CustomerFullName;
            txtTelephone.Text = customer.Telephone;
            txtEmail.Text = customer.EmailAddress;
            dpDob.Text = customer.CustomerBirthday.ToString();
            var status = customer.CustomerStatus;
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

        private void ButtonBase_OnClickAdd(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(dpDob.Text))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show("Customer is already exist. Do you want to proceed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                customer.CustomerFullName = txtName.Text;
                customer.Telephone = txtTelephone.Text;
                customer.EmailAddress = txtEmail.Text;
                customer.CustomerBirthday = DateOnly.Parse(dpDob.Text);
                customer.CustomerStatus = active.IsChecked == true ? (byte)1 : (byte)0;
                context.Customers.Update(customer);
                context.SaveChanges();
                Load();
                return;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void ButtonBase_OnClickPass(object sender, RoutedEventArgs e)
        {
            Window popupWindow = new Window
            {
                Title = "Pop up",
                Content = new Page12(), // Set the content to Page6
                SizeToContent = SizeToContent.WidthAndHeight, // Size window to content
                ResizeMode = ResizeMode.CanResizeWithGrip // Optional: Prevent resizing
            };
            popupWindow.Show();
            popupWindow.Closed += (sender, e) => Load();
        }
    }
}
