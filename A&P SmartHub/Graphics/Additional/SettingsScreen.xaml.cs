using System;
using System.Collections.Generic;
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
    }
}
