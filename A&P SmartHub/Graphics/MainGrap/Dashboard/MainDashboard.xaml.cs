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

        private HomePage _homePage;
        private AllDevices _allDevices;
        private MenuPage _menuPage;

        public MainDashboard()
        {
            InitializeComponent();
            _homePage = new HomePage();
            _allDevices = new AllDevices();
            _menuPage = new MenuPage();
            MainDisplay.Content = _homePage;
        }

        private async void SlideAnimation(UserControl newScreen)
        {
           
            newScreen.Opacity = 0;
            TranslateTransform slide = new TranslateTransform(0, 30);
            newScreen.RenderTransform = slide;

          
            MainDisplay.Content = newScreen;

          

          
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
            SlideAnimation(_homePage);
        }

        private void AllDevices_Click(object sender, RoutedEventArgs e)
        {
            SlideAnimation(_allDevices);
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            SlideAnimation(_menuPage);
        }

    }
}