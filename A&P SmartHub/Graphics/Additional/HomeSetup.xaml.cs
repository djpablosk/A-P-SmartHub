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
        public string HomeName {  get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        

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


    }
}
