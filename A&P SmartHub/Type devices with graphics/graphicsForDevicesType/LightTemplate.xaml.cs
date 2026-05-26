using System;
using System.Collections.Generic;
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
using A_P_SmartHub.Graphics.Additional;
using System.Windows.Controls.Primitives;

namespace A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType
{
    /// <summary>
    /// Interaction logic for LightTemplate.xaml
    /// </summary>
    public partial class LightTemplate : UserControl
    {

        public LightTemplate(DeviceType device)
        {
            InitializeComponent();
            this.DataContext = device;
            this.Loaded += LightControl_Loaded;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is Grid && ((Grid)parent).Name == "PopupOverlay"))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is Grid overlay)
            {
                overlay.Visibility = Visibility.Collapsed;
            }
        }

        private void BrightnessSlider_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            BrightnessSlider.Value -= e.Delta > 0 ? 1 : -1;
        }



        private async void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SmartHubRAM.SavecurrentBrightnessLight = e.NewValue;

            if (BrightnessText != null)
            {
                BrightnessText.Text = $"{Math.Round(e.NewValue)}%";
            }


            int brightness = (int)e.NewValue;

            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(2);
                    string espIpAddress = "192.168.0.205"; // Nezabudni doplniť vašu IPčku

                    string url = $"http://{espIpAddress}/set_brightness?v={brightness}";

                    await client.GetStringAsync(url);
                }
            }
            catch { /* Ignorujeme, ak potiahneš slider a ESP zrovna neodpovedá */ }
        }
        private void LightControl_Loaded(object sender, RoutedEventArgs e)
        {
            BrightnessSlider.ValueChanged -= BrightnessSlider_ValueChanged;
            BrightnessSlider.RemoveHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(BrightnessSlider_DragCompleted));

            BrightnessSlider.Value = SmartHubRAM.SavecurrentBrightnessLight;

            if (BrightnessText != null)
            {
                BrightnessText.Text = $"{Math.Round(SmartHubRAM.SavecurrentBrightnessLight)}%";
            }

            BrightnessSlider.ValueChanged += BrightnessSlider_ValueChanged;
            BrightnessSlider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(BrightnessSlider_DragCompleted));
        }

        private void BrightnessSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            SmartHubRAM.SavecurrentBrightnessLight = BrightnessSlider.Value;

        }


        private async Task SetEspLedColor(int r, int g, int b)
        {
            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    string espIpAddress = "192.168.0.205"; //ipcka esp !!MENI SA!!
                    string url = $"http://{espIpAddress}/set_led?r={r}&g={g}&b={b}";
                    await client.GetStringAsync(url);
                }
            }
            catch { 
            MessageBox.Show("Failed to connect to ESP32. Please check the IP address and ensure the device is online.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            SetEspLedColor(255, 255, 255);
        }

        private async void MorningScene_Click(object sender, RoutedEventArgs e)
        {
            await SetEspLedColor(255, 200, 100);
        }
        private async void EveningScene_Click(object sender, RoutedEventArgs e)
        {
            await SetEspLedColor(255, 140, 50);
        }
        private async void NightScene_Click(object sender, RoutedEventArgs e)
        {
            await SetEspLedColor(10, 10, 30);
        }
        private async void DayScene_Click(object sender, RoutedEventArgs e)
        {
            await SetEspLedColor(255, 255, 200);
        }

        private void RGB_Click(object sender, RoutedEventArgs e)
        {
            RGB_SelectorWindow rgbWindow = new RGB_SelectorWindow();
            rgbWindow.ShowDialog();
        }
    }
}
