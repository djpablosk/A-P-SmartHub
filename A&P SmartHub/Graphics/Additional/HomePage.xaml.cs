using A_P_SmartHub.AI;
using A_P_SmartHub.Databazicky;
using A_P_SmartHub.Graphics.Additional;
using A_P_SmartHub.Graphics.Login;
using A_P_SmartHub.Graphics.MainGrap;
using A_P_SmartHub.spotify;
using A_P_SmartHub.Type_devices_with_graphics;
using A_P_SmartHub.Type_devices_with_graphics.graphicsForDevicesType;
using A_P_SmartHub.Weather;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
using static A_P_SmartHub.Graphics.MainGrap.Dashboard.MainDashboard;

namespace A_P_SmartHub.Graphics.Additional
{
    public partial class HomePage : UserControl
    {
        MySql sql1 = new MySql();
        getData data = new getData();
        bool notsafeMessageShown = false;
        Chatbot Chatbot = new Chatbot();
        DispatcherTimer timer = new DispatcherTimer();
        smtpClientMail mail = new smtpClientMail();
        SpotifyConnector connector = new SpotifyConnector();

        bool isFirstRUN = true;
        bool isOfflineAlertShown = false;

        public string City { get; set; }
        public ObservableCollection<DeviceType> MyDevices { get; set; }
        public ObservableCollection<AlertMessage> SystemAlerts { get; set; }
        



        private static readonly HttpClient _httpClient = new HttpClient();
        DispatcherTimer espTimer = new DispatcherTimer();
        private const string _espAddress = "http://192.168.0.110/data"; // tu sa IP adresa !!MENI!! 
        DateTime checkTime = DateTime.MinValue;


        public class AlertMessage
        {
            public string Time { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
        }
      

        public HomePage()
        {
   
            InitializeComponent();



            spotifybutton.Visibility = Visibility.Hidden;

            MyDevices = new ObservableCollection<DeviceType>();

            SystemAlerts = new ObservableCollection<AlertMessage>();
            AlertsList.ItemsSource = SystemAlerts;


            DeviceList.ItemsSource = SmartHubRAM.RecentDevices;

            LoadFromDB();
            LoadTestData();

            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += async (s, e) =>
            {
                Greet();
                await Islogged();
                await connector.LoadCurrentlyPlaying();
                currentlyPlaying.Content = SmartHubRAM.currentlyPlaying;
                await UpdateWeather();
              
               
            };
           

            espTimer.Interval = TimeSpan.FromSeconds(3);
            espTimer.Tick += async (s, e) =>
            {
                await FetchEspData();
                
            };
            espTimer.Start();
           

            _ = InitSpotifyThenStartTimer();

        }
        private async Task InitSpotifyThenStartTimer()
        {
            await sql1.ReturnSpotifyRefresh(SessionInfo.ID);// pockam kym sa nacita z databaze refresher
            if (!string.IsNullOrEmpty(SmartHubRAM.SpotifyRefreshKey)// ak refresher nie je prazdny
                && SmartHubRAM.SpotifyRefreshKey != "Err404")//alebo sa nerovna err404 = stale nie je preazdny
                await connector.RefreshAccessToken();// pockam kym sa ziska novy acces token

            timer.Start(); // az potom zacnem timer cize nacitavanie pocasia a currently listnening
        }

        private async Task FetchEspData()
        {
            try
            {
                using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(3)))
                {
                    string response = await _httpClient.GetStringAsync(_espAddress, cts.Token);
                    string[] data = response.Split(',');

                    if (data.Length == 5)
                    {
                        float temp = float.Parse(data[0], System.Globalization.CultureInfo.InvariantCulture);
                        float hum = float.Parse(data[1], System.Globalization.CultureInfo.InvariantCulture);
                        int gasRaw = int.Parse(data[2]);
                        float heatIndex = float.Parse(data[3], System.Globalization.CultureInfo.InvariantCulture);
                        int gasPercent = int.Parse(data[4]);

                        if (IndoorTempText != null) IndoorTempText.Text = $"{temp}°C";
                        if (IndoorHumidityText != null) IndoorHumidityText.Text = $"{hum}%";
                        if (AirQualityValueText != null) AirQualityValueText.Text = $"{gasPercent}";
                        if (GasRawValueText != null) GasRawValueText.Text = $"{gasRaw}";
                        if (HeatIndexText != null) HeatIndexText.Text = $"{heatIndex}°C";


                        if (AirQualityText != null)
                        {
                            if (gasPercent < 15)
                            {
                                AirQualityText.Text = "Good";
                                AirQualityText.Foreground = new SolidColorBrush(Colors.Green);
                            }
                            else if (gasPercent >= 15 && gasPercent < 40)
                            {
                                AirQualityText.Text = "Moderate";
                                AirQualityText.Foreground = new SolidColorBrush(Colors.Orange);
                            }
                            else 
                            {
                                AirQualityText.Foreground = new SolidColorBrush(Colors.Red);
                                AirQualityText.Text = "DANGER";

                                if (isFirstRUN || (DateTime.Now - checkTime).TotalMinutes >= 3)
                                {
                                    isFirstRUN = false;
                                    checkTime = DateTime.Now;

                             
                                    await mail.GasAlert(SessionInfo.Mail, sql1.UserName, sql1.HomeName, gasPercent);

                                    SystemAlerts.Insert(0, new AlertMessage
                                    {
                                        Time = DateTime.Now.ToString("HH:mm:ss"),
                                        Title = "SMART HUB ALERT",
                                        Message = $"Gas sensor detected unsafe air quality levels ({gasPercent}%)."
                                    });
                                }
                            }
                        }

                     
                        EspDataText.Text = "Online";
                        isOfflineAlertShown = false; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ESP data: {ex.Message}");

                EspDataText.Text = "Offline";
                AirQualityText.Text = "-";
                IndoorTempText.Text = "-";
                IndoorHumidityText.Text = "-";
                AirQualityValueText.Text = "-";
                GasRawValueText.Text = "-";
                HeatIndexText.Text = "-";

             
                if (!isOfflineAlertShown)
                {
                    isOfflineAlertShown = true; 

                    SystemAlerts.Insert(0, new AlertMessage
                    {
                        Time = DateTime.Now.ToString("HH:mm:ss"),
                        Title = "CONNECTION LOST",
                        Message = "SmartHub lost connection with ESP32 hardware module. Retrying automatically..."
                    });
                }
             

                if (!espTimer.IsEnabled)
                {
                    espTimer.Start();
                }
            }
        }



        private async Task LoadTestData()
        {
            string id = SessionInfo.ID;
           await sql1.ReturnBasicFromDB(id);

            var name = sql1.HomeName;
            var devices = await sql1.LoadDevices(id);

            foreach (var device in devices)
            {
                var newdevice = new DeviceType();
                newdevice.DeviceName = device.DeviceName;
                newdevice.Type = device.Type;


                MyDevices.Add(newdevice);
            }


        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new A_P_SmartHub.Graphics.Login.Login(), true);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button stlaceneButton = sender as Button;
            if (stlaceneButton == null) return;

            DeviceType stlaceneDevice = stlaceneButton.DataContext as DeviceType;

            if (stlaceneDevice != null)
            {

                if (SmartHubRAM.RecentDevices.Contains(stlaceneDevice))
                {
                    SmartHubRAM.RecentDevices.Remove(stlaceneDevice);
                }


                SmartHubRAM.RecentDevices.Insert(0, stlaceneDevice);


                if (SmartHubRAM.RecentDevices.Count > 5)
                {
                    SmartHubRAM.RecentDevices.RemoveAt(5);
                }


                try
                {
                    switch (stlaceneDevice.Type)
                    {
                        case DeviceTypeEnum.Light:
                            var lightWindow = new LightTemplate(stlaceneDevice);
                            PopupContent.Content = lightWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Toggle:
                            var toggleWindow = new ToggleTemplate(stlaceneDevice);
                            PopupContent.Content = toggleWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Climate:
                            var climateWindow = new ClimateTemplate(stlaceneDevice);
                            PopupContent.Content = climateWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Cover:
                            var coverWindow = new CoverTemplate(stlaceneDevice);
                            PopupContent.Content = coverWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;

                        case DeviceTypeEnum.Media:
                            var mediaWindow = new MediaTemplate(stlaceneDevice);
                            PopupContent.Content = mediaWindow;
                            PopupOverlay.Visibility = Visibility.Visible;
                            break;
                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading device template: {ex.Message}");
                }
            }
        }

        public async void LoadFromDB()
        {
            string id = SessionInfo.ID;
            await sql1.ReturnBasicFromDB(id);
      

            dashHomeName.Text = sql1.HomeName;
            City = sql1.City;
            await UpdateWeather();
            Greet();

            string LengthCheck = dashHomeName.Text;
            if (LengthCheck.Length == 0)
            {
                dashHomeName.Text = "Defaultne Meno";
            }
        }

        public async Task UpdateWeather()
        {
            await data.getTemperature(City);
            WeatherCity.Text = City;
            WeatherTemp.Text = $"{data.Temperature}°C";
        }

        public void Greet()
        {
            if (DateTime.Now.Hour <= 11)
            {
                WelcomeBack.Text = $"Good Morning, {sql1.UserName} !";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 19)
            {
                WelcomeBack.Text = $"Good Afternoon, {sql1.UserName} !";
            }
            else
            {
                WelcomeBack.Text = $"Good Evening, {sql1.UserName} !";
            }
        }


        private void AddNewDevice_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new AddDeviceMainDashboard(), true);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
          await  connector.SpotifyLogin();
            
           await sql1.SpotifyLogin(SessionInfo.ID, SmartHubRAM.SpotifyRefreshKey,true);


            
        }


        private void AIButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new AI_Screen(), true);
            }

        }
        public async Task Islogged()
        {
            await sql1.IsLogged(SessionInfo.ID);
            if (sql1.Islogged_ == true)
            {
                spotifybutton.Visibility = Visibility.Hidden;
                currentlyPlaying.Visibility = Visibility.Visible;
            }
            else if (!sql1.Islogged_)
            {
                spotifybutton.Visibility = Visibility.Visible;
                currentlyPlaying.Visibility = Visibility.Hidden;
            }
        }
    }
}