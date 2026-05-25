using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Type_devices_with_graphics;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace A_P_SmartHub.Graphics.Additional
{
    /// <summary>
    /// Interaction logic for AddNewDeviceWindow.xaml
    /// </summary>
    public partial class AddNewDeviceWindow : Window
    {
       
        MySql mySql = new MySql();
        MySQL_Users mySqlUsers = new MySQL_Users();
        public AddNewDeviceWindow()
        {
            InitializeComponent();
        }

       

        public SmartDevice NewDevice { get; set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = mySqlUsers.GetUserId(SessionInfo.Mail);
                var selected = deviceType.SelectedItem as ComboBoxItem;
                string devtype = selected?.Content.ToString();
                if (string.IsNullOrWhiteSpace(devtype) ||
     string.IsNullOrWhiteSpace(DeviceNameBox.Text) ||
     string.IsNullOrWhiteSpace(DeviceIPAddressBox.Text))
                {
                    MessageBox.Show("Please Fill all fields");
                    return;
                }
                else
                {
                    NewDevice = new SmartDevice
                    {
                        Name = DeviceNameBox.Text,
                        Type = deviceType.Text,
                        IpAddress = DeviceIPAddressBox.Text
                    };
                    await mySql.AddDevice(id, DeviceNameBox.Text, DeviceIPAddressBox.Text, devtype);
                }
            }
            catch
            {
                MessageBox.Show("Unknown error try again");
            }

            this.DialogResult = true;
        }

        private void DeviceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
