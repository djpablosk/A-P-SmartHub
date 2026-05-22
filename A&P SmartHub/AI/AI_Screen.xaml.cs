using A_P_SmartHub.Graphics.MainGrap.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace A_P_SmartHub.AI
{
    /// <summary>
    /// Interaction logic for AI_Screen.xaml
    /// </summary>
    public partial class AI_Screen : UserControl
    {
      
        public ObservableCollection<ChatMessage> ChatHistory { get; set; } = new ObservableCollection<ChatMessage>();
        public AI_Screen()
        {
            InitializeComponent();
            InitializeAI();
        }
        public class ChatMessage
        {
            public string Text { get; set; }
            public bool IsUser { get; set; }
           
            public HorizontalAlignment Alignment => IsUser ? HorizontalAlignment.Right : HorizontalAlignment.Left;


        
            public SolidColorBrush BgColor => IsUser ?
                (SolidColorBrush)new BrushConverter().ConvertFrom("#1ABC9C") :
                (SolidColorBrush)new BrushConverter().ConvertFrom("#2A3441");
        }
        private void InitializeAI()
        {
            ChatItemsControl.ItemsSource = ChatHistory;
            ChatHistory.Add(new ChatMessage
            {
                Text = "Hello! I am James your AI assistant",
                IsUser = false
            });
        }


        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            await ProcessUserMessage();
        }

        private async void ChatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await ProcessUserMessage();
            }
        }

        private async Task ProcessUserMessage()
        {
            string userText = ChatInputBox.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            ChatHistory.Add(new ChatMessage { Text = userText, IsUser = true });
            ChatInputBox.Clear();
            ScrollToBottom();

            // 2. Tu sa volá metóda, do ktorej Alex napojí API!
             string aiResponse = await GetAIResponseFromAPI(userText);
            
           
             ChatHistory.Add(new ChatMessage { Text = aiResponse, IsUser = false });
            ScrollToBottom();
        }
        private async Task <string> GetAIResponseFromAPI(string userText)
        {
            Chatbot Bot = new Chatbot();
            return await Bot.AiChat(userText);

        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.SlideViewTransition(new MainDashboard(), true);
            }
        }
    }
}
