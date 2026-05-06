using A_P_SmartHub.Type_devices_with_graphics;
using A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for AllDevices.xaml
    /// </summary>
    public partial class AllDevices : UserControl
    {
        public ObservableCollection<DeviceType> MyDevices { get; set; }
        public AllDevices()
        {
            InitializeComponent();
            MyDevices = new ObservableCollection<DeviceType>();
            DeviceList.ItemsSource = MyDevices;
            LoadTestData();

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

                    case DeviceTypeEnum.Toggles:
                        var toggleWindow = new ToggleTemplate(stlaceneDevice);
                        PopupContent.Content = toggleWindow;
                        PopupOverlay.Visibility = Visibility.Visible;
                        break;

                    case DeviceTypeEnum.Climates:
                        var climateWindow = new ClimateTemplate(stlaceneDevice);
                        PopupContent.Content = climateWindow;
                        PopupOverlay.Visibility = Visibility.Visible;
                        break;

                    case DeviceTypeEnum.Covers:
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
        }

        public void LoadTestData()
        {
            // Vytvárame nové zariadenia a hádžeme ich do zoznamu
            MyDevices.Add(new DeviceType { ID = 1, Name = "Stolná Lampa", Type = DeviceTypeEnum.Lights });
            MyDevices.Add(new DeviceType { ID = 2, Name = "Kuchynský LED Pás", Type = DeviceTypeEnum.Lights });
            MyDevices.Add(new DeviceType { ID = 3, Name = "Klimatizácia", Type = DeviceTypeEnum.Climates });
            MyDevices.Add(new DeviceType { ID = 4, Name = "Zasuvka", Type = DeviceTypeEnum.Toggles });
            MyDevices.Add(new DeviceType { ID = 5, Name = "Žalúzia", Type = DeviceTypeEnum.Covers });
            MyDevices.Add(new DeviceType { ID = 6, Name = "TV", Type = DeviceTypeEnum.Media });
        }

    }
}
