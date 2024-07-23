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
using Main_Project.Models;

namespace Question2
{
    /// <summary>
    /// Interaction logic for Page12.xaml
    /// </summary>
    ///
    public partial class Page12 : Page
    {
        private readonly Customer customer = MainWindow.INSTANCE._customer;
        public Page12()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPass.Password;
            string newPassword = txtNewPass.Password;
            if(BCrypt.Net.BCrypt.Verify(oldPassword, customer.Password))
            {
                customer.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                MainWindow.INSTANCE.context.Update(customer);
                MainWindow.INSTANCE.context.SaveChanges();
                MessageBox.Show("Password changed successfully!", "Password changed", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Old password is incorrect!", "Incorrect Password", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            CloseParentWindow();
        }
        private void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}
