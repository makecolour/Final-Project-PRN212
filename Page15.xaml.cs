using Main_Project.Models;
using Microsoft.EntityFrameworkCore;
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

namespace Main_Project
{
    /// <summary>
    /// Interaction logic for Page15.xaml
    /// </summary>
    public partial class Page15 : Page
    {
        private readonly Input input = new Input();
        private readonly EmailService emailService = new EmailService(MainWindow.INSTANCE.configuration);
        private Customer c = new Customer();
        public Page15()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            String email = this.txtEmail.Text;
            if (!string.IsNullOrWhiteSpace(email))
            {
                var customer = MainWindow.INSTANCE.context.Customers.FirstOrDefault(x => EF.Functions.Like(x.EmailAddress, email));
                if (customer != null)
                {
                    c = customer;
                    string code = input.generateRandomString(6);
                    await emailService.SendEmailAsync(email, "Verification Code", $"Your verification code is {code}");
                    Page14 page14 = new Page14(code);
                    page14.VerificationSuccess += Page14_VerificationSuccess;
                    Window popupWindow = new Window
                    {
                        Title = "Pop up",
                        Content = page14,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        ResizeMode = ResizeMode.CanResizeWithGrip
                    };
                    popupWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Page14_VerificationSuccess(object? sender, EventArgs e)
        {
            txtEmail.IsReadOnly = true;
            var window = Window.GetWindow(this) as Popup;
           window.Content = new Page16(c);
           window.SizeToContent = SizeToContent.WidthAndHeight;
           window.ResizeMode = ResizeMode.CanResizeWithGrip;
           window.Show();
        }
    }
}
