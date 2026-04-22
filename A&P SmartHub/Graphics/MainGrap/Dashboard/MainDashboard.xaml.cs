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

        private HomePage homePage;
        private AllDevices allDevices;
        private MenuPage menuPage;

        public MainDashboard()
        {
            InitializeComponent();
            homePage = new HomePage();
            allDevices = new AllDevices();
            menuPage = new MenuPage();
            MainDisplay.Content = homePage;
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
            SlideAnimation(homePage);
        }

        private void AllDevices_Click(object sender, RoutedEventArgs e)
        {
            SlideAnimation(allDevices);
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            SlideAnimation(menuPage);
        }

    }
}

