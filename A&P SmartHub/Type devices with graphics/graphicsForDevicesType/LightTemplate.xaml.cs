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

       

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SmartHubRAM.SavecurrentBrightnessLight = e.NewValue;

            if (BrightnessText != null)
            {
                BrightnessText.Text = $"{Math.Round(e.NewValue)}%";
            }
        }
        private void LightControl_Loaded(object sender, RoutedEventArgs e)
        {
            BrightnessSlider.ValueChanged -= BrightnessSlider_ValueChanged;

            BrightnessSlider.Value = SmartHubRAM.SavecurrentBrightnessLight;

            if (BrightnessText != null)
            {
                BrightnessText.Text = $"{Math.Round(SmartHubRAM.SavecurrentBrightnessLight)}%";
            }

            BrightnessSlider.ValueChanged += BrightnessSlider_ValueChanged;
        }
    }
}
