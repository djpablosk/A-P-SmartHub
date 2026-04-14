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

