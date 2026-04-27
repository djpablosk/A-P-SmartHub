using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for HomeSetup.xaml
    /// </summary>
        public class SmartDevice
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string IpAddress { get; set; }
        }
    public partial class HomeSetup : UserControl
    {

        public ObservableCollection<SmartDevice> TempDevices = new ObservableCollection<SmartDevice>();
        public HomeSetup()
        {
            InitializeComponent();
            deviceList.ItemsSource = TempDevices;
        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            //AddNewDeviceWindow addDeviceWindow = new AddNewDeviceWindow();
            //addDeviceWindow.ShowDialog();
            var addWindow = new AddNewDeviceWindow();
            if (addWindow.ShowDialog() == true)
            {
                TempDevices.Add(addWindow.NewDevice); // Pridá sa do zoznamu a hneď sa zjaví Border
            }
        }
        
        private void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var device = btn.Tag as SmartDevice;
            TempDevices.Remove(device); 
        }

        private void CreateHome_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreatingProfileLoading();
        }


    }
}
