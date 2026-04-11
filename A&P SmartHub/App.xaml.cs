using SQLitePCL;
using System.Windows;

namespace A_P_SmartHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Batteries.Init();
        }
    }

}
