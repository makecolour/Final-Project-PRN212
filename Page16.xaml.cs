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
using Main_Project.Models;
using Question2;

namespace Main_Project
{
    /// <summary>
    /// Interaction logic for Page16.xaml
    /// </summary>
    public partial class Page16 : Page
    {
        private Customer customer;
        public Page16(Customer c)
        {
            customer = c;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtRetype.Password;
            string newPassword = txtNewPass.Password;
            if (oldPassword == newPassword)
            {
                customer.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                MainWindow.INSTANCE.context.Update(customer);
                MainWindow.INSTANCE.context.SaveChanges();
                MessageBox.Show("Password changed successfully!", "Password changed", MessageBoxButton.OK);
                CloseParentWindow();
            }
            else
            {
                MessageBox.Show("Retype password is incorrect!", "Incorrect Password", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            
        }
        private void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}
