using Question2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {

        public Page3()
        {
            InitializeComponent();
            SubPageFrame.Navigate(new Page2(1));
            Load();
        }

        private void Load()
        {
            
            var list = MainWindow.INSTANCE.context.Customers.OrderByDescending(x => x.CustomerStatus).ToList();
            Customers.ItemsSource = list;
        }


        private void Customers_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           var item = (sender as DataGrid).SelectedItem;
           if(item!=null)
           {
               var customer = item as Customer;
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
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "";
            txtId.Text = "";
            txtName.Text = "";
            txtTelephone.Text = "";
            dpDob.Text = "";
            deactive.IsChecked = false;
            active.IsChecked = false;
        }

        private void ButtonBase_OnClickAdd(object sender, RoutedEventArgs e)
        {
            var listEmail = MainWindow.INSTANCE.context.Customers.Where(x => x.EmailAddress == txtEmail.Text).ToList();
            if (!string.IsNullOrWhiteSpace(txtId.Text)&&!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                int id = int.Parse(txtId.Text);
                var list = MainWindow.INSTANCE.context.Customers.Where(x => x.CustomerId == id).ToList();
                
                if (list.Count <= 0)
                {
                    if (listEmail.Count > 0)
                    {
                        MessageBox.Show("Input another email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var customer = new Customer()
                    {
                        CustomerFullName = txtName.Text,
                        Telephone = txtTelephone.Text,
                        EmailAddress = txtEmail.Text,
                        CustomerBirthday = DateOnly.Parse(dpDob.Text),
                        CustomerStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                        BookingReservations = new List<BookingReservation>()
                    };
                    MainWindow.INSTANCE.context.Customers.Add(customer);
                    MainWindow.INSTANCE.context.SaveChanges();
                    Load();
                    return;
                }
                else
                {
                    var result = MessageBox.Show("Customer is already exist. Do you want to proceed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var customer = list.FirstOrDefault();
                        customer.CustomerId = id;
                        customer.CustomerFullName = txtName.Text;
                        customer.Telephone = txtTelephone.Text;
                        customer.EmailAddress = txtEmail.Text;
                        customer.CustomerBirthday = DateOnly.Parse(dpDob.Text);
                        customer.CustomerStatus = active.IsChecked == true ? (byte)1 : (byte)0;
                        MainWindow.INSTANCE.context.Customers.Update(customer);
                        MainWindow.INSTANCE.context.SaveChanges();
                        Load();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
           
            }
            else
            {
                if (listEmail.Count > 0 || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Input another email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var customer = new Customer()
                {
                    CustomerFullName = txtName.Text,
                    Telephone = txtTelephone.Text,
                    EmailAddress = txtEmail.Text,
                    CustomerBirthday = DateOnly.Parse(dpDob.Text),
                    CustomerStatus = active.IsChecked == true ? (byte)1 : (byte)0,
                    BookingReservations = new List<BookingReservation>()
                };
                MainWindow.INSTANCE.context.Customers.Add(customer);
                MainWindow.INSTANCE.context.SaveChanges();
                Load();
                return;
            }
        }


        private void ButtonBase_OnClickDel(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var list = MainWindow.INSTANCE.context.Customers.Where(x => x.CustomerId == id).ToList();
                if (list.Count <= 0)
                {
                   MessageBox.Show("Customer does not exist!");
                }
                else
                {
                    Customer customer = MainWindow.INSTANCE.context.Customers.Find(id);
                    customer.CustomerStatus = 0;
                    MainWindow.INSTANCE.context.SaveChanges();
                    Load();
                }

            }
            else
            {
                MessageBox.Show("Customer ID cannot be blank!");
                return;
            }
        }

        private void ButtonBase_OnClickPass(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                int id = int.Parse(txtId.Text);
                var list = MainWindow.INSTANCE.context.Customers.Where(x => x.CustomerId == id).ToList();
                if (list.Count <= 0)
                {
                    MessageBox.Show("Customer does not exist!");
                }
                else
                {
                    Customer customer = MainWindow.INSTANCE.context.Customers.Find(id);
                    customer.Password = BCrypt.Net.BCrypt.HashPassword(MainWindow.INSTANCE.configuration["default_password"]);
                    MainWindow.INSTANCE.context.SaveChanges();
                    Load();
                }

            }
            else
            {
                MessageBox.Show("Customer ID cannot be blank!");
                return;
            }
        }
    }
}
