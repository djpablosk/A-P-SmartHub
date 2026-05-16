using A_P_SmartHub.Databazicky;
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
using System.Windows.Shapes;

namespace A_P_SmartHub.Graphics.MainGrap.Registration
{
    /// <summary>
    /// Interaction logic for WelcomeWidow.xaml
    /// </summary>
    public partial class WelcomeWidow : Window
    {
        MySql MySql = new MySql();

        public  WelcomeWidow()
        {
            InitializeComponent();
            WelcomeUser();
        }

        public async Task WelcomeUser()
        {
            await MySql.ReturnBasicFromDB(SessionInfo.ID);
            //   MessageBox.Show(SessionInfo.ID);
            string name = MySql.UserName;
            //  MessageBox.Show(name);


            WelcomeText.Text = $"Glad to have you here, {name}!";
        }
    }
}
