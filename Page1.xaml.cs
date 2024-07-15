using Microsoft.Extensions.Configuration;
using Question2.Models;
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

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        private IConfigurationRoot configuration;
       
        public Page1()
        {
            InitializeComponent();
            LoadAppSettings();
        }
        private void LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            String email = this.email.Text;
            String password = this.password.Password;
            var adminEmail = configuration["Admin:email"];
            var adminPassword = configuration["Admin:password"];
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                if (email == adminEmail && password == adminPassword)
                {
                    NavigationService.Navigate(new Page3());
                    MessageBox.Show("Admin login successful!");
                }
                else if (context.Customers.FirstOrDefault(x => x.EmailAddress == email && x.Password == password && x.CustomerStatus==1) != null)
                {
                    MainWindow.INSTANCE._customer = context.Customers.FirstOrDefault(x => x.EmailAddress == email && x.Password == password);
                    NavigationService.Navigate(new Page9());
                    MessageBox.Show("Customer login successful!");
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter both email and password.");
            }
        }
    }
}
