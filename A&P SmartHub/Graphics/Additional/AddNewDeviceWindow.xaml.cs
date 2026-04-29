using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for AddNewDeviceWindow.xaml
    /// </summary>
    public partial class AddNewDeviceWindow : Window
    {
        SQLITE_Users sQLITE_Users = new SQLITE_Users();
        MySql mySql = new MySql();
        public AddNewDeviceWindow()
        {
            InitializeComponent();
        }

       

        public SmartDevice NewDevice { get; set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            NewDevice = new SmartDevice
            {
                Name = DeviceNameBox.Text,
                Type = deviceType.Text,
                IpAddress = DeviceIPAddressBox.Text
            };
           
            string id = sQLITE_Users.GetUserId(SessionInfo.Mail);

            await mySql.AddDevice(id, DeviceNameBox.Text, DeviceIPAddressBox.Text, deviceType.SelectedItem.ToString());

            this.DialogResult = true;
        }

        private void DeviceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
