using A_P_SmartHub.Databazicky;
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
    /// Interaction logic for AddDeviceMainDashboard.xaml
    /// </summary>
    public partial class AddDeviceMainDashboard : UserControl
    {
        public ObservableCollection<SmartDevice> TempDevices = new ObservableCollection<SmartDevice>();


        private List<SmartDevice> DevicesToDelete = new List<SmartDevice>();
        public AddDeviceMainDashboard()
        {
            InitializeComponent();
            deviceList.ItemsSource = TempDevices;
            this.Loaded += AddDeviceMainDashboard_Loaded;
        }

        private async void AddDeviceMainDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDevicesFromDB();
        }
        private async Task LoadDevicesFromDB()
        {
            string mail = SessionInfo.Mail;
            MySQL_Users users = new MySQL_Users();
            string id = users.GetUserId(mail);

            MySql sql = new MySql();

           
            var existingDevices = await sql.LoadDevices(id);

            TempDevices.Clear();

            
            foreach (var d in existingDevices)
            {
                SmartDevice device = new SmartDevice();

                
                device.Name = d.DeviceName;
                device.Type = d.Type.ToString(); 
                device.IsNew = false; 

                TempDevices.Add(device);
            }
        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddNewDeviceWindow();
            if (addWindow.ShowDialog() == true)
            {
                addWindow.NewDevice.IsNew = true;
                TempDevices.Add(addWindow.NewDevice);
            }
        }

        private void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var device = btn.Tag as SmartDevice;
            TempDevices.Remove(device);
        }




        //public async Task SaveToDB()
        //{
        //    string mail = SessionInfo.Mail;
        //    MySQL_Users users = new MySQL_Users();
        //    string id = users.GetUserId(mail);

        //    MySql sql = new MySql();

        //    foreach (var dev in DevicesToDelete)
        //    {
        //        await sql.DeleteDevice(id, dev.Name);
        //    }
        //    DevicesToDelete.Clear();

        //    foreach (var device in TempDevices)
        //    {
        //        if (device.IsNew)
        //        {
        //            await sql.AddDevice(id, device.Name, device.Type);
        //            device.IsNew = false;
        //        }
        //    }
        //}




        
        private async void CreateHome_Click(object sender, RoutedEventArgs e)
        {


            //await SaveToDB();
            this.Content = new CreatingProfileLoading();

        }
    }
}
