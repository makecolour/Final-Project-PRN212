using Microsoft.Extensions.Configuration;
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
using Microsoft.EntityFrameworkCore;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
       
        public Page1()
        {
            InitializeComponent();
            MainWindow.INSTANCE.isAdmin = false;
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string email = this.email.Text;
            string password = this.password.Password;
            var adminEmail = MainWindow.INSTANCE.configuration["Admin:email"];
            var adminPassword = MainWindow.INSTANCE.configuration["Admin:password"];
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                if (email == adminEmail && password == adminPassword)
                {
                    MainWindow.INSTANCE.isAdmin = true;
                    NavigationService.Navigate(new Page3());
                    MessageBox.Show("Admin login successful!");
                }
                else
                {
                    MainWindow.INSTANCE.isAdmin = false;
                    foreach (var customer in MainWindow.INSTANCE.context.Customers.ToList())
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, customer.Password)&&customer.CustomerStatus==1&&customer.EmailAddress==email)
                        {
                            MainWindow.INSTANCE._customer = customer;
                            NavigationService.Navigate(new Page9());
                            MessageBox.Show("Customer login successful!");
                            return;
                        }
                    }
                    MessageBox.Show("Invalid email or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Please enter both email and password.");
            }
        }

        private void ButtonBase_OnClickRegister(object sender, RoutedEventArgs e)
        {
            Window popupWindow = new Popup()
            {
                Title = "Pop up",
                Content = new Page13(), // Set the content to Page6
                SizeToContent = SizeToContent.WidthAndHeight, // Size window to content
                ResizeMode = ResizeMode.CanResizeWithGrip // Optional: Prevent resizing
            };
            popupWindow.Show();
        }

        private void ButtonBase_OnClickForget(object sender, RoutedEventArgs e)
        {
            Window popupWindow = new Popup()
            {
                Title = "Pop up",
                Content = new Page15(), // Set the content to Page6
                SizeToContent = SizeToContent.WidthAndHeight, // Size window to content
                ResizeMode = ResizeMode.CanResizeWithGrip // Optional: Prevent resizing
            };
            popupWindow.Show();
        }
    }
}
