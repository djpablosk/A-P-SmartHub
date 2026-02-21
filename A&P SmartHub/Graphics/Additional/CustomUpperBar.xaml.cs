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
    /// Interaction logic for CustomUpperBar.xaml
    /// </summary>
    public partial class CustomUpperBar : UserControl
    {
        public CustomUpperBar()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void MaxRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            var okno = Window.GetWindow(this);
            if (okno.WindowState == WindowState.Normal)
                okno.WindowState = WindowState.Maximized;
            else
                okno.WindowState = WindowState.Normal;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }
    }
}
