using A_P_SmartHub.Graphics.Additional;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A_P_SmartHub.Graphics.MainGrap.Dashboard
{
    /// <summary>
    /// Interaction logic for MainDashboard.xaml
    /// </summary>
    public partial class MainDashboard : UserControl
    {
        HomeSetup setup;
        public MainDashboard()
        {
            InitializeComponent();
            DashboardHomeName.Text = "DefaultName =-=";

            var myDevices = new List<SmartDevice>
    {
        new SmartDevice { Name = "Main Light" },
        new SmartDevice { Name = "Thermostat" },
        new SmartDevice { Name = "TV Living Room" },
        new SmartDevice { Name = "Led Strip" },
        new SmartDevice { Name = "Humidifier" },
        new SmartDevice { Name = "PC Station" },
        new SmartDevice { Name = "Camera 1" },
        new SmartDevice { Name = "Router" }
    };

            // A tu to pripojíme na ten náš XAML zoznam
            DeviceList.ItemsSource = myDevices;
        }

        private void SlideAnimation(UserControl newScreen)
        {
            MainDisplay.Content = newScreen;

           
            TranslateTransform slide = new TranslateTransform();
            newScreen.RenderTransform = slide;
            
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(900), 
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } 
            };

           
            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = 30,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(900),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

           
            newScreen.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            slide.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
        }

        public void Uprava()
        {
            
            DashboardHomeName.Text = setup.HomeName;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming soon");
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            SlideAnimation(menuPage);
        }

    }
}

