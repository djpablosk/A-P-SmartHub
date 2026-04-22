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

        private async void SlideAnimation(UserControl newScreen)
        {
            // 1. Zneviditeľníme ju a posunieme ju rovno o 30px nižšie
            newScreen.Opacity = 0;
            TranslateTransform slide = new TranslateTransform(0, 30);
            newScreen.RenderTransform = slide;

            // 2. Vložíme ju do hlavného okna (WPF začne zbesilo počítať tiene a farby)
            MainDisplay.Content = newScreen;

            // 3. TOTO JE TEN MAGICKÝ TRIK: Pauza na 50 milisekúnd.
            // Dáme grafike čas všetko vykresliť, kým začneme hýbať oknom.
            await Task.Delay(50); // Tip: Ak by to stále trochu trhlo, skús prepísať na 100

            // 4. Obrazovka je plne načítaná v pamäti, spúšťame plynulé animácie
            DoubleAnimation fadeAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(900),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Timeline.SetDesiredFrameRate(fadeAnimation, 60);

            DoubleAnimation slideAnimation = new DoubleAnimation
            {
                From = 30,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(900),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Timeline.SetDesiredFrameRate(slideAnimation, 60);

            // Vystrelenie
            newScreen.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
            slide.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
        }



        private void HomePage_Click(object sender, RoutedEventArgs e)
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

