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
using System.Windows.Threading;

namespace A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType
{
    /// <summary>
    /// Interaction logic for ClimateTemplate.xaml
    /// </summary>
    public partial class ClimateTemplate : UserControl
    {
        private double _currentTemp = 25.0;
        private double _targetTemp = 27.0;
        private DispatcherTimer _tempTimer;
        private string _status = "AUTO";
        public ClimateTemplate(DeviceType climate)
        {
            InitializeComponent();
            this.DataContext = climate;
            _tempTimer = new DispatcherTimer();
            _tempTimer.Interval = TimeSpan.FromMilliseconds(2000);
            _tempTimer.Tick += TempTimer_Tick;
            _targetTemp = SmartHubRAM.SavecurrentTemperatureClimate;
            _currentTemp = SmartHubRAM.SavecurrentTemperatureClimate;
            _status = SmartHubRAM.SavecurrentstatClimate;
            CurrentStat.Text = _status;
            UpdateUI();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is Grid && ((Grid)parent).Name == "PopupOverlay"))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }


            if (parent is Grid overlay)
            {
                overlay.Visibility = Visibility.Collapsed;
            }
            SmartHubRAM.SavecurrentTemperatureClimate = _targetTemp;
            SmartHubRAM.SavecurrentTemperatureClimate = _currentTemp;
            SmartHubRAM.SavecurrentstatClimate = _status;
        }


        private void ButtonPlus_click(object sender, RoutedEventArgs e)
        {
            _targetTemp += 1;
            UpdateUI();
            _tempTimer.Start();

        }

        private void ButtonMinus_click(object sender, RoutedEventArgs e)
        {
            _targetTemp -= 1;
            UpdateUI();
            _tempTimer.Start();
        }

        private void TempTimer_Tick(object sender, EventArgs e)
        {
            if (_currentTemp < _targetTemp)
            {
                _currentTemp += 1;
            }
            else if (_currentTemp > _targetTemp)
            {
                _currentTemp -= 1;
            }

            CurrentTemp.Text = $"{_currentTemp} °C";

            if (_currentTemp == _targetTemp)
            {
                _tempTimer.Stop();
            }
        }
        private void UpdateUI()
        {
            CurrentTemp.Text = $"{_currentTemp} °C";
            TargetTemp.Text = $"/ {_targetTemp} °C";
        }


        private void AUTOstat_click(object sender, RoutedEventArgs e)
        {
            CurrentStat.Text = "AUTO";
            _status = "AUTO";
           
        }
        private void COOLstat_click(object sender, RoutedEventArgs e)
        {
            CurrentStat.Text = "Cooling";
            _status = "Cooling";
            
        }
        private void HEATstat_click(object sender, RoutedEventArgs e)
        {
            CurrentStat.Text = "Heating";
            _status = "Heating";
        }
        private void ECOstat_click(object sender, RoutedEventArgs e)
        {
            CurrentStat.Text = "ECO";
            _status = "ECO";
        }
    }
}
