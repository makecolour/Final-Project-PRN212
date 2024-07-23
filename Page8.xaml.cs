using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Page8.xaml
    /// </summary>
    public partial class Page8 : Page
    {
        private event PropertyChangedEventHandler PropertyChanged;
        private FontWeight _mngCustomer = FontWeights.Normal;
        private FontWeight _mngRoom = FontWeights.Normal;
        private bool _isCustomer = false;
        private bool _isRoom = false;
        public FontWeight mngCustomer
        {
            get { return _mngCustomer; }
            set
            {
                if (_mngCustomer != value)
                {
                    _mngCustomer = value;
                    OnPropertyChanged(nameof(mngCustomer));
                }
            }
        }

        public FontWeight mngRoom
        {
            get { return _mngRoom; }
            set
            {
                if (_mngRoom != value)
                {
                    _mngRoom = value;
                    OnPropertyChanged(nameof(mngRoom));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page8(int x)
        {
            InitializeComponent();
            DataContext = this;
            switch (x)
                {
                case 2:
                    mngCustomer = FontWeights.Bold;
                    break;
                case 1:
                    mngRoom = FontWeights.Bold;
                    break;
                default:
                    break;
            }
                
            
        }

        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page11());
        }

        private void ManageReservation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page5());
        }

        private void MenuItem_OnClickLogout(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE._customer = null;
            MainWindow.INSTANCE.MainFrame.Navigate(new Page1());
        }
    }
}
