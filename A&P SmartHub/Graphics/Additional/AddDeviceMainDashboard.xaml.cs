using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.MainGrap.Dashboard;
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
        MySQL_Users users = new MySQL_Users();

        MySql sql = new MySql();
        private async void AddDeviceMainDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDevicesFromDB();
        }
        private async Task LoadDevicesFromDB()
        {
            string mail = SessionInfo.Mail;
          
            string id = users.GetUserId(mail);


           
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

        public void UpdateUser()
        {
            string homeName = string.IsNullOrEmpty(HomeName.Text) ? null : HomeName.Text;
            string Cityname = string.IsNullOrEmpty(CityName.Text) ? null : CityName.Text;
            sql.UpdateUser(homeName, null, Cityname, SessionInfo.ID);
          //  string Username = string.IsNullOrEmpty(Username) ? null : Username.Text; -- musim pockat kym pato prida lebo je reatard
        }




        public async Task SaveToDB()
        {
            string mail = SessionInfo.Mail;
            MySQL_Users users = new MySQL_Users();
            string id = SessionInfo.ID;

            MySql sql = new MySql();

            foreach (var dev in DevicesToDelete)
                // toto nikde nevolas pato..
            {
                await sql.DeleteDevice(id, dev.IpAddress);
            }
            DevicesToDelete.Clear();

            foreach (var device in TempDevices)
            {
                if (device.IsNew)
                {
// idk akoze co tu pato varil
                }
            }
        }

        



        
        private async void CreateHome_Click(object sender, RoutedEventArgs e)
        {//save buttton
            UpdateUser();
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new MainDashboard(), true);
            }
        }

        private void HomeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //
        }
    }
}
