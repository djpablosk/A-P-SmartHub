using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A_P_SmartHub.Type_devices_with_graphics
{
    /// <summary>
    /// Interaction logic for RGB_SelectorWindow.xaml
    /// </summary>
    public partial class RGB_SelectorWindow : Window
    {
        private readonly string espIpAddress = "192.168.1.100";
        public RGB_SelectorWindow()
        {
            InitializeComponent();
        }



        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ColorPreview == null) return;

            byte r = (byte)RedSlider.Value;
            byte g = (byte)GreenSlider.Value;
            byte b = (byte)BlueSlider.Value;

            ColorPreview.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        private async void ApplyColor_Click(object sender, RoutedEventArgs e)
        {
            await SetEspLedColor((int)RedSlider.Value, (int)GreenSlider.Value, (int)BlueSlider.Value);
        }

        private async void BtnQuickRed_Click(object sender, RoutedEventArgs e)
        {
            RedSlider.Value = 255; GreenSlider.Value = 0; BlueSlider.Value = 0;
            await SetEspLedColor(255, 0, 0);
        }

        private async void BtnQuickGreen_Click(object sender, RoutedEventArgs e)
        {
            RedSlider.Value = 0; GreenSlider.Value = 255; BlueSlider.Value = 0;
            await SetEspLedColor(0, 255, 0);
        }

        private async void BtnQuickBlue_Click(object sender, RoutedEventArgs e)
        {
            RedSlider.Value = 0; GreenSlider.Value = 0; BlueSlider.Value = 255;
            await SetEspLedColor(0, 0, 255);
        }

        private async Task SetEspLedColor(int r, int g, int b)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    string url = $"http://{espIpAddress}/set_led?r={r}&g={g}&b={b}";
                    await client.GetStringAsync(url);
                }
            }
            catch {
            MessageBox.Show("Failed to connect to ESP32. Please check the IP address and ensure the device is online.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
