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
    /// Interaction logic for SettingsScreen.xaml
    /// </summary>
    public partial class SettingsScreen : UserControl
    {
        public SettingsScreen()
        {
            InitializeComponent();
            LoadFakeMaintenanceData();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // Handle delete account logic here
            MessageBox.Show("Delete Account button clicked.");
        }   

        private void AlertToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle toggle button logic here
           MessageBox.Show("Toggle button clicked.");
        }

        public class MaintenanceAlert
        {
            public string Message { get; set; }
            public string Time { get; set; }
            public string IconColor { get; set; }
            public string Icon { get; set; }
        }

        public ObservableCollection<MaintenanceAlert> FakeAlerts { get; set; } = new ObservableCollection<MaintenanceAlert>();

        private void LoadFakeMaintenanceData()
        {
            FakeAlerts.Add(new MaintenanceAlert
            {
                Icon = "⚠️",
                IconColor = "#F59E0B", 
                Message = "Gas sensor (Kitchen) requires routine calibration.",
                Time = "10 mins ago"
            });

            FakeAlerts.Add(new MaintenanceAlert
            {
                Icon = "🔋",
                IconColor = "#EF4444", 
                Message = "Low battery in the front door sensor (15%).",
                Time = "2 hours ago"
            });

            FakeAlerts.Add(new MaintenanceAlert
            {
                Icon = "✅",
                IconColor = "#10B981", 
                Message = "A&P SmartHub firmware v1.2.4 successfully installed.",
                Time = "Yesterday"
            });

           
            if (MaintenanceList != null)
            {
                MaintenanceList.ItemsSource = FakeAlerts;
            }
        }

        private void HomeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //public async Task UpdateUser()
            //{
            //    string homeName = string.IsNullOrEmpty(HomeName.Text) ? null : HomeName.Text;
            //    string Cityname = string.IsNullOrEmpty(CityName.Text) ? null : CityName.Text;
            //    string userName = string.IsNullOrEmpty(UserName.Text) ? null : UserName.Text;
            //await   sql.UpdateUser(homeName, userName, Cityname, SessionInfo.ID);
            //  //  string Username = string.IsNullOrEmpty(Username) ? null : Username.Text; -- musim pockat kym pato prida lebo je reatard
            //} potom treba pridat ked patrik headtrick  upravi rozpolozenie
                
        }
    }
}
