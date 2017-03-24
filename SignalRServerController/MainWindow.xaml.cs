using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Test_SignalR;

namespace SignalRServerController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerDisplayViewModel _serverDisplayViewModel = new ServerDisplayViewModel();
        HubConnection _hubConnection;
        private IHubProxy _hubProxy;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _serverDisplayViewModel;
            _hubConnection = new HubConnection("http://localhost:54457/signalr/hubs");
            _hubProxy = _hubConnection.CreateHubProxy("myChatHub");
            _hubConnection.Start().Wait();
            _hubProxy.Invoke("setName", "Dektop Chat Client").Wait();
            _hubProxy.On<string>("addMessage", OnAddMessage);
            

        }

        private void OnAddMessage(string theMessage)
        {
            _serverDisplayViewModel.ReceievedData = _serverDisplayViewModel.ReceievedData + "\n" + theMessage;
        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendCurrentMessage();
        }


        private void Input_TextBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendCurrentMessage();
            }
        }

        private void SendCurrentMessage()
        {
            _hubProxy.Invoke("send", _serverDisplayViewModel.ChatText).Wait();
            _serverDisplayViewModel.ChatText = "";

        }
    }
}
