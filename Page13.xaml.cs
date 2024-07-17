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
using Question2;
using Question2.Models;

namespace Main_Project
{
    /// <summary>
    /// Interaction logic for Page13.xaml
    /// </summary>
    public partial class Page13 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        public Page13()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrEmpty(txtRetypePass.Password) || string.IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(txtPass.Password != txtRetypePass.Password)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var customer = new Customer()
            {
                CustomerFullName = txtName.Text,
                EmailAddress = txtEmail.Text,
                Password = txtPass.Password,
                Telephone = txtTelephone.Text,
                CustomerStatus = 0
            };
            if(dpDob.SelectedDate!= null)
            {
                customer.CustomerBirthday = DateOnly.FromDateTime(dpDob.SelectedDate.Value);
            }
            context.Customers.Add(customer);
            context.SaveChanges();
            MessageBox.Show("Customer added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseParentWindow();
        }
        private void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}
