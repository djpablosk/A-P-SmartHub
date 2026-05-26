using A_P_SmartHub.Databazicky;
using MySqlConnector;
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
      MySql sql = new MySql();
        MySQL_Users users = new MySQL_Users();
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
        { //
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await UpdateUser();
                HomeName.Text = "";
                CityName.Text = "";
                UserName.Text = "";
                MailBox.Text = "";
                MessageBox.Show("Your account has been updated. You were logged out to refresh your session. Please sign in again.");
                SessionInfo info = new SessionInfo(); // v podstate mazem ram aby stary pouzivatel nemal tieto infos keby nahodou
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);

                }
            }
            catch { MessageBox.Show("unknown error"); }

        }
        public async Task UpdateUser()
        {
            string homeName = string.IsNullOrEmpty(HomeName.Text) ? null : HomeName.Text;
            string Cityname = string.IsNullOrEmpty(CityName.Text) ? null : CityName.Text;
            string userName = string.IsNullOrEmpty(UserName.Text) ? null : UserName.Text;
            string Mail = string.IsNullOrEmpty(MailBox.Text) ? null : MailBox.Text;
            if (users.IsMailInDB(Mail))
            {
                MessageBox.Show("Looks like this mail is already connected to another account please try again");
                return;
            }

            await users.ChangeMail(Mail, SessionInfo.ID);
            // akoze viem ze by to chcelo este smtp ale uz to nestiham lebo som extremne unaveny to bude dalsi update <33


            await sql.UpdateUser(homeName,userName,Cityname,SessionInfo.ID);
        }
      
        //  string Username = string.IsNullOrEmpty(Username) ? null : Username.Text; -- musim pockat kym pato prida lebo je reatard

    }
}


