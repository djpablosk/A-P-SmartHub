using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Databazicky;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
using static A_P_SmartHub.Graphics.MainGrap.Dashboard.MainDashboard;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            LoadFromDB();

            var myDevices = new List<SmartDevice>
    {
        new SmartDevice { Name = "Main Light" },
        new SmartDevice { Name = "Thermostat" },
        new SmartDevice { Name = "TV Living Room" },
        new SmartDevice { Name = "Led Strip" },
        new SmartDevice { Name = "Humidifier" },
        new SmartDevice { Name = "PC Station" },
        new SmartDevice { Name = "Camera 1" },
        new SmartDevice { Name = "Router" }
    };




            // A tu to pripojíme na ten náš XAML zoznam
            DeviceList.ItemsSource = myDevices;
        }
        public class SmartDevice
        {
            public string Name { get; set; }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;


            if (mainWindow != null)
            {

                mainWindow.SlideViewTransition(new SettingsScreen(), true);
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;


            if (mainWindow != null)
            {

                mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
        }

        public async void LoadFromDB()
        {
            MySql sql = new MySql();
            string id = SessionInfo.ID;
           await sql.ReturnBasicFromDB(id);
            dashHomeName.Text = sql.HomeName.ToString();
            string LengthCheck = dashHomeName.Text;
            if (LengthCheck.Length == 0)
            {
                dashHomeName.Text = "Defaultne Meno";
            }

          
            
        
        }
    }
}

