using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Weather;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Threading;
using static A_P_SmartHub.Graphics.MainGrap.Dashboard.MainDashboard;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        getData data = new getData();
        public string City { get; set; }
        public HomePage()
        {

            InitializeComponent();
           
           LoadFromDB(data);

            timer.Interval = TimeSpan.FromMinutes(2); //na update casu som pouzil ai (zakomentujem '*')
            timer.Tick += async (s, e) => //*
            {
                await UpdateWeather();  // *
            };
            timer.Start();//*
            
            
           

           

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

        public async Task LoadFromDB(getData getData)
        {
           
            MySql sql = new MySql();
            string id = SessionInfo.ID;
           
            await sql.ReturnBasicFromDB(id);
            City = sql.City;
            MessageBox.Show(sql.City);
            dashHomeName.Text = sql.HomeName;
           
            string LengthCheck = dashHomeName.Text;
         
            if (LengthCheck.Length == 0)
            {
                dashHomeName.Text = "Defaultne Meno";
            }
            await UpdateWeather();
          

        }

        public async Task UpdateWeather()
        { // toto uz nie je ai
            await data.getTemperature(City);
            Weather.Text = $"Current Temperature In {City} is {data.Temperature} °C";
        }
    }
}

