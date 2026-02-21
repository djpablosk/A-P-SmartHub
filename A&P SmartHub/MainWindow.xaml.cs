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
            if (usernamebox_Copy1.Text == "Username...")
                usernamebox_Copy1.Clear();

        }
    }
}