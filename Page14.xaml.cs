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
using BCrypt.Net;

namespace Main_Project
{
    /// <summary>
    /// Interaction logic for Page14.xaml
    /// </summary>
    public partial class Page14 : Page
    {
        private string _code;
        public event EventHandler VerificationSuccess;

        private void OnVerificationSuccess()
        {
            VerificationSuccess?.Invoke(this, EventArgs.Empty);
        }
        public Page14(string code)
        {
            InitializeComponent();
            _code = code;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string check = txtVerifyCode.Text;
            if (check == _code)
            {
                MessageBox.Show("Verification successful!", "Verification successful", MessageBoxButton.OK);
                OnVerificationSuccess();
                CloseParentWindow();
                
            }
            else
            {
                MessageBox.Show("Verification failed!", "Verification failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }
    }
}
