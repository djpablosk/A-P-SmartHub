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
    /// Interaction logic for CreatingProfileLoading.xaml
    /// </summary>
    public partial class CreatingProfileLoading : UserControl
    {
        public CreatingProfileLoading()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string[] loadingMessages = new string[]
            {
                "Creating your profile...",
                "Setting up your devices...",
                "Finalizing the setup..."
            };

            foreach (string message in loadingMessages)
            {
                TextLoading.Text = message;
                await System.Threading.Tasks.Task.Delay(5000); 
            }

        }
    }
}
