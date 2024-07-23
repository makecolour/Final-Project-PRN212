using System.Configuration;
using System.IO;
using System.Text;
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
using Microsoft.Extensions.Configuration;
using Question2;

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly FuminiHotelManagementContext context = FuminiHotelManagementContext.INSTANCE;
        public Customer _customer { get; set; }
        public bool isAdmin { get; set; }
        public IConfigurationRoot configuration;
        private static MainWindow _instance;
        public static MainWindow INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainWindow();
                }
                return _instance;
            }
            
        }
        private void LoadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();
        }
        public MainWindow()
        {
            LoadAppSettings();
            _instance = this;
            isAdmin = false;
            InitializeComponent();
            MainFrame.Navigate(new Page1());
        }
        
    }
}