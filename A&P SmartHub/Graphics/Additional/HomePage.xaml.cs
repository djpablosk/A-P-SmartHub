using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType;
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
        public ObservableCollection<DeviceType> MyDevices { get; set; }
        public HomePage()
        {
            InitializeComponent();

            MyDevices = new ObservableCollection<DeviceType>();

            DeviceList.ItemsSource = MyDevices;
            LoadTestData();
        }
        private void LoadTestData()
        {
            // Vytvárame nové zariadenia a hádžeme ich do zoznamu
            MyDevices.Add(new DeviceType { ID = 1, Name = "Stolná Lampa", Type = DeviceTypeEnum.Lights });
            MyDevices.Add(new DeviceType { ID = 2, Name = "Kuchynský Pás", Type = DeviceTypeEnum.Lights });
            MyDevices.Add(new DeviceType { ID = 3, Name = "Termostat Obývačka", Type = DeviceTypeEnum.Climates });
            MyDevices.Add(new DeviceType { ID = 4, Name = "Kávovar", Type = DeviceTypeEnum.Toggles });
        }
            LoadFromDB();

  




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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button stlaceneButton = sender as Button;
            DeviceType stlaceneDevice = stlaceneButton.DataContext as DeviceType;

            if (stlaceneDevice != null)
            {
                switch (stlaceneDevice.Type)
                {
                    case DeviceTypeEnum.Lights:
                        // Oprava chyby CS1503: Posielame presne ten typ, ktorý okno čaká
                        var lightWindow = new LightTemplate(stlaceneDevice);
                        PopupContent.Content = lightWindow;
                        PopupOverlay.Visibility = Visibility.Visible;
                        break;
                }
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

