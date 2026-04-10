using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for HomeSetup.xaml
    /// </summary>
    public partial class HomeSetup : UserControl
    {
        private ObservableCollection<string> Rooms { get; } = new ObservableCollection<string>();
        private ObservableCollection<string> Devices { get; } = new ObservableCollection<string>();
        private Dictionary<string, ObservableCollection<string>> Assigned { get; } = new Dictionary<string, ObservableCollection<string>>();
        private string SelectedRoom { get; set; }

        public HomeSetup()
        {
            InitializeComponent();
            DevicesList.ItemsSource = Devices;
            RoomPreview.ItemsSource = Assigned;

            Rooms.CollectionChanged += (s, e) => RefreshRoomsWrap();
            RefreshRoomsWrap();
        }

        private void AddRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = (NewRoomBox.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name)) { MessageBox.Show("Enter a room name."); return; }
            if (Rooms.Contains(name)) { MessageBox.Show("Room already exists."); return; }

            Rooms.Add(name);
            Assigned[name] = new ObservableCollection<string>(); // <-- ensure dictionary entry
            RefreshPreview();
            RefreshRoomsWrap();
            NewRoomBox.Clear();
        }

        private void AddDeviceBtn_Click(object sender, RoutedEventArgs e)
        {
            var name = (NewDeviceBox.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Enter a device name.");
                return;
            }
            if (Devices.Contains(name) || Assigned.Values.Any(c => c.Contains(name)))
            {
                MessageBox.Show("Device already added.");
                return;
            }
            Devices.Add(name);
            NewDeviceBox.Clear();
        }

        private void AssignBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedRoom) || !(DevicesList.SelectedItem is string device))
            {
                MessageBox.Show("Select a room (click a room tile) and a device to assign.");
                return;
            }

            Devices.Remove(device);
if (!Assigned.TryGetValue(SelectedRoom, out var roomDevices))
{
    roomDevices = new ObservableCollection<string>();
     Assigned[SelectedRoom] = roomDevices;
}
roomDevices.Add(device);
            RefreshPreview();
            RefreshRoomsWrap();
        }

        private void UnassignBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedRoom))
            {
                MessageBox.Show("Select a room to unassign a device from.");
                return;
            }

            var roomDevices = Assigned[SelectedRoom];
            if (roomDevices.Count == 0)
            {
                MessageBox.Show("Selected room has no devices.");
                return;
            }

            // remove last assigned device (simple selection-free unassign)
            var device = roomDevices.Last();
            roomDevices.Remove(device);
            Devices.Add(device);
            RefreshPreview();
            RefreshRoomsWrap();
        }

        private void SaveHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            var houseName = (HouseNameBox.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(houseName))
            {
                MessageBox.Show("Please enter a name for your home.");
                return;
            }

            // For now just show confirmation. Persisting is out of scope for UI work.
            MessageBox.Show($"Home '{houseName}' saved. Rooms: {Rooms.Count}, Devices assigned: {Assigned.Sum(kv => kv.Value.Count)}");
        }

        private void RefreshPreview()
        {
            // Rebind dictionary to refresh ItemsControl
            RoomPreview.ItemsSource = null;
            RoomPreview.ItemsSource = Assigned;
        }

        private void RefreshRoomsWrap()
        {
            RoomsWrap.Children.Clear();
            foreach (var r in Rooms)
            {
                var border = new Border
                {
                    Width = 200,
                    Height = 140,
                    Margin = new Thickness(8),
                    CornerRadius = new CornerRadius(10),
                    Background = r == SelectedRoom ? (System.Windows.Media.Brush)new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(83,255,220)) : (System.Windows.Media.Brush)new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(23,32,43))
                };

                var stack = new StackPanel { Margin = new Thickness(10) };
                var title = new TextBlock { Text = r, Foreground = System.Windows.Media.Brushes.White, FontSize = 18, FontWeight = FontWeights.SemiBold };
                Assigned.TryGetValue(r, out var devices);
                var countText = $"Devices: {devices?.Count ?? 0}";
                var count = new TextBlock { Text = countText, Foreground = Brushes.LightGray, Margin = new Thickness(0,6,0,0) };
                stack.Children.Add(title);
                stack.Children.Add(count);
                border.Child = stack;

                border.MouseLeftButtonUp += (s, e) =>
                {
                    SelectedRoom = r;
                    RefreshRoomsWrap();
                };

                RoomsWrap.Children.Add(border);
            }
        }

        private void QuickAddLivingRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!Rooms.Contains("Living Room"))
            {
                Rooms.Add("Living Room");
                Assigned["Living Room"] = new ObservableCollection<string>();
                RefreshRoomsWrap();
            }
        }

        private void QuickAddKitchen_Click(object sender, RoutedEventArgs e)
        {
            if (!Rooms.Contains("Kitchen"))
            {
                Rooms.Add("Kitchen");
                Assigned["Kitchen"] = new ObservableCollection<string>();
                RefreshRoomsWrap();
            }
        }
    }
}
