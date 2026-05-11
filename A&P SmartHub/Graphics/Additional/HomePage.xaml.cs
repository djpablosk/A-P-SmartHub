using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Weather;
using A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType;
using A_P_SmartHub.Databazicky;
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
using System.Collections.ObjectModel;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Type_devices_with_graphics;
using A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType;

using static A_P_SmartHub.Graphics.MainGrap.Dashboard.MainDashboard;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    /// 

    public partial class HomePage : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();
        getData data = new getData();
        MySql sql1 = new MySql();
        public string City { get; set; }
        public ObservableCollection<DeviceType> MyDevices { get; set; }
        public HomePage()
        {

            InitializeComponent();

            MyDevices = new ObservableCollection<DeviceType>();

            DeviceList.ItemsSource = MyDevices;

            LoadTestData();
            LoadFromDB();



            timer.Interval = TimeSpan.FromMinutes(2); //na update casu som pouzil ai (zakomentujem '*')
            timer.Tick += async (s, e) => //*
            {
                await UpdateWeather();  // *
            };
            timer.Start();//*

        }
        private async Task LoadTestData()
        {

            string id = SessionInfo.ID;
            var name = sql1.HomeName;
            var devices = await sql1.LoadDevices(id);
            foreach (var device in devices)
            {
                var newdevice = new DeviceType();

                newdevice.DeviceName = device.DeviceName;
                newdevice.Type = device.Type;

                MyDevices.Add(newdevice);
                DeviceList.ItemsSource = MyDevices;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button stlaceneButton = sender as Button;
            DeviceType stlaceneDevice = stlaceneButton.DataContext as DeviceType;

            if (stlaceneDevice != null)
            {
                try
                {

                    switch (stlaceneDevice.Type)
                    {
                        case DeviceTypeEnum.Light:
                            var lightWindow = new LightTemplate(stlaceneDevice);
                            PopupContent.Content = lightWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Toggle:
                            var toggleWindow = new ToggleTemplate(stlaceneDevice);
                            PopupContent.Content = toggleWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Climate:
                            var climateWindow = new ClimateTemplate(stlaceneDevice);
                            PopupContent.Content = climateWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Cover:
                            var coverWindow = new CoverTemplate(stlaceneDevice);
                            PopupContent.Content = coverWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Media:
                            var mediaWindow = new MediaTemplate(stlaceneDevice);
                            PopupContent.Content = mediaWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading device template: {ex.Message}");
                }
            }
        }


        public async void LoadFromDB()
        {


            string id = SessionInfo.ID;

            sql1.ReturnBasicFromDB(id);

            await sql1.LoadDevices(id);
            dashHomeName.Text = sql1.HomeName;
            City = sql1.City;
            await UpdateWeather();



            string LengthCheck = dashHomeName.Text;

            if (LengthCheck.Length == 0)
            {
                dashHomeName.Text = "Defaultne Meno";
            }




        }

        public async Task UpdateWeather()
        { // toto uz nie je ai
            await data.getTemperature(City);
            WeatherCity.Text = City;
            WeatherTemp.Text = $"{data.Temperature}°C";
        }
    }
}

