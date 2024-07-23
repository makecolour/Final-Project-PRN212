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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private event PropertyChangedEventHandler PropertyChanged;
        private FontWeight _mngCustomer = FontWeights.Normal;
        private FontWeight _mngRoom = FontWeights.Normal;
        private FontWeight _mngBooking = FontWeights.Normal;
        private FontWeight _mngReport = FontWeights.Normal;
        private bool _isCustomer = false;
        private bool _isRoom = false;
        private bool _isBooking = false;
        private bool _isReport = false;

        public bool isCustomer
        {
            get { return _isCustomer; }
            set
            {
                if (_isCustomer != value)
                {
                    _isCustomer = value;
                    OnPropertyChanged(nameof(isCustomer));
                }
            }
        }
        public bool isRoom  
        {
            get { return _isRoom; }
            set
            {
                if (_isRoom != value)
                {
                    _isRoom = value;
                    OnPropertyChanged(nameof(isRoom));
                }
            }
        }
        public bool isBooking
        {
            get { return _isBooking; }
            set
            {
                if (_isBooking != value)
                {
                    _isBooking = value;
                    OnPropertyChanged(nameof(isBooking));
                }
            }
        }
        public bool isReport
        {
            get { return _isReport; }
            set
            {
                if (_isReport != value)
                {
                    _isReport = value;
                    OnPropertyChanged(nameof(isReport));
                }
            }
        }

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

        public FontWeight mngBooking
        {
            get { return _mngBooking; }
            set
            {
                if (_mngBooking != value)
                {
                    _mngBooking = value;
                    OnPropertyChanged(nameof(mngBooking));
                }
            }
        }
        public FontWeight mngReport
        {
            get { return _mngReport; }
            set
            {
                if (_mngReport != value)
                {
                    _mngReport = value;
                    OnPropertyChanged(nameof(mngReport));
                }
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page2(int x)
        {
            InitializeComponent();
            DataContext = this;
            switch (x)
            {
                case 1:
                    mngCustomer = FontWeights.Bold;
                    isCustomer = true;
                    break;
                case 2:
                    mngRoom = FontWeights.Bold;
                    isRoom = true;
                    break;
                case 3:
                    mngBooking = FontWeights.Bold;
                    isBooking = true;
                    break;
                case 4:
                    mngReport = FontWeights.Bold;
                    isReport = true;
                    break;
                default:
                    break;
            }
        }
        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page3());
        }

        private void ManageRoom_Click(object sender, RoutedEventArgs e)
        {
            // Code to open the Manage Room page or window
            MainWindow.INSTANCE.MainFrame.Navigate(new Page4());
        }

        private void ManageBooking_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page5());
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page7());
        }

        private void MenuItem_OnClickLogout(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.MainFrame.Navigate(new Page1());
        }
    }
}
