using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Login;
using SQLitePCL;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace A_P_SmartHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainDisplay.Content = new Login();
            UpperBar.Content = new CustomUpperBar();
            this.WindowState = WindowState.Maximized;

            
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);


            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void usernamebox_Copy1_GotFocus_1(object sender, RoutedEventArgs e)
        {
            //if (usernamebox_Copy1.Text == "Username...")
            // usernamebox_Copy1.Clear();

        }

        public void SlideViewTransition(UserControl newView, bool v)    //animacia blur 
        {

            BlurEffect blur = new BlurEffect()
            {
                Radius = 0
            };
            MainDisplay.Effect = blur;

            TimeSpan duration = TimeSpan.FromSeconds(0.15);


            DoubleAnimation blurOut = new DoubleAnimation(0, 20, duration);
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, duration);

            blurOut.Completed += (s, e) =>
            {
                MainDisplay.Content = newView;
                DoubleAnimation blurIn = new DoubleAnimation(50, 0, duration);
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, duration);

                blur.BeginAnimation(BlurEffect.RadiusProperty, blurIn);
                MainDisplay.BeginAnimation(OpacityProperty, fadeIn);
            };

            blur.BeginAnimation(BlurEffect.RadiusProperty, blurOut);
            MainDisplay.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
