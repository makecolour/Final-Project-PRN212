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
    /// Interaction logic for Page12.xaml
    /// </summary>
    /// 
    public partial class Page12 : Page
    {
        private FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        private Customer customer = MainWindow.INSTANCE._customer;
        public Page12()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPass.Password;
            string newPassword = txtNewPass.Password;
            if(oldPassword== customer.Password)
            {
                customer.Password = newPassword;
                context.Update(customer);
                context.SaveChanges();
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
