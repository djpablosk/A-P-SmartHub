using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.MainGrap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                TempDevices.Add(addWindow.NewDevice); 
            }
        }
        
        private void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var device = btn.Tag as SmartDevice;
            TempDevices.Remove(device); 
        }

        


        public async Task SaveToDB()
        {
            string mail = SessionInfo.Mail;

            SQLITE_Users users = new SQLITE_Users();
            string id = users.GetUserId(mail);

            SessionInfo.ID = id;

            MySql sql = new MySql();
            await sql.SaveToDB(id, HomeName.Text, UserName.Text, City.Text);
        }


 

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // Await the asynchronous database operation
                    await SaveToDB();
                MessageBox.Show("pokracovanie nabuduce");
            }
            catch (Exception ex)
            {
                // Shows the error if the database save fails (e.g., missing .env variables, connection failure)
                MessageBox.Show($"Error saving to database: {ex.Message}");
            }
        }

        private async void CreateHome_Click(object sender, RoutedEventArgs e)
        {
              
        
            await SaveToDB();
            this.Content = new CreatingProfileLoading();
        
    }
    }
}