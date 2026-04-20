using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.MainGrap;
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
    public partial class HomeSetup : UserControl
    {


        public HomeSetup()
        {
            InitializeComponent();

        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewDeviceWindow addDeviceWindow = new AddNewDeviceWindow();
            addDeviceWindow.ShowDialog();
        }
        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            AddRoomWindow addRoomWindow = new AddRoomWindow();
            addRoomWindow.ShowDialog();
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveToDB();
        }
    }
}