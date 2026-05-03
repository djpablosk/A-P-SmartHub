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
    /// Interaction logic for ToggleTemplate.xaml
    /// </summary>
    public partial class ToggleTemplate : UserControl
    {
        public ToggleTemplate(DeviceType toggle)
        {
            InitializeComponent();
            this.DataContext = toggle;
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
    }
}
