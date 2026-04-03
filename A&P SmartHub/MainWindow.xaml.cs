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


        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Začne presúvanie okna, keď stlačíš ľavé tlačidlo myši
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

        public void SlideViewTransition(UserControl newView, bool slideLeftToRight)
        {
            // 1. Zabezpečíme, že náš kontajner sa vôbec môže hýbať (pridáme mu Transform)
            if (!(MainDisplay.RenderTransform is TranslateTransform))
            {
                MainDisplay.RenderTransform = new TranslateTransform();
            }
            TranslateTransform trans = (TranslateTransform)MainDisplay.RenderTransform;

            // Vypočítame, kam to má odletieť (napr. 800 pixelov mimo obrazovku)
            double distance = slideLeftToRight ? 800 : -800;

            // 2. Animácia ODCHODU (Starý pohľad letí preč)
            DoubleAnimation slideOut = new DoubleAnimation(0, distance, TimeSpan.FromSeconds(0.4));
            // Easing je to isté, ako keď v DaVinci zakrivíš Spline (aby to malo pekný rozbeh)
            slideOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };

            // Keď animácia odchodu skončí, urobíme "strih"
            slideOut.Completed += (s, e) =>
            {
                // Vymeníme UserControl (Login za Register)
                MainDisplay.Content = newView;

                // 3. Animácia PRÍCHODU (Nový pohľad letí dnu z opačnej strany)
                DoubleAnimation slideIn = new DoubleAnimation(-distance, 0, TimeSpan.FromSeconds(0.4));
                slideIn.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

                // Spustíme let dnu
                trans.BeginAnimation(TranslateTransform.XProperty, slideIn);
            };

            // Spustíme let von
            trans.BeginAnimation(TranslateTransform.XProperty, slideOut);
        }
    }
}
