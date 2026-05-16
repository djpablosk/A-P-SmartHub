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

namespace A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType
{
    /// <summary>
    /// Interaction logic for MediaTemplate.xaml
    /// </summary>
    public partial class MediaTemplate : UserControl
    {
        public MediaTemplate(DeviceType media)
        {
            InitializeComponent();
            this.DataContext = media;
            this.Loaded += MediaControl_Loaded;
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

        private void VolumeSlider_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            VolumeSlider.Value -= e.Delta > 0 ? 1 : -1;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SmartHubRAM.SavecurrentVolumeLevel = e.NewValue;
            if (VolumeText != null)
            {
                VolumeText.Text = $"{Math.Round(e.NewValue)}%";
            }
        }


        private void MediaControl_Loaded(object sender, RoutedEventArgs e)
        {
           VolumeSlider.ValueChanged -= VolumeSlider_ValueChanged;
           VolumeSlider.Value = SmartHubRAM.SavecurrentVolumeLevel;
            if(VolumeText != null)
            {
                VolumeText.Text = $"{Math.Round(SmartHubRAM.SavecurrentVolumeLevel)}%";
            }

           VolumeSlider.ValueChanged += VolumeSlider_ValueChanged;
        }
    }
}
