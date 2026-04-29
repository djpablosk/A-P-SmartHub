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
using System.Windows.Shapes;
using A_P_SmartHub.Graphics.Additional;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for AddNewDeviceWindow.xaml
    /// </summary>
    public partial class AddNewDeviceWindow : Window
    {
        public AddNewDeviceWindow()
        {
            InitializeComponent();
        }

       

        public SmartDevice NewDevice { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NewDevice = new SmartDevice
            {
                Name = DeviceName.Text,
                Type = deviceType.Text,
                IpAddress = DeviceIPAddress.Text
            };
            this.DialogResult = true;
        }
    }
}
