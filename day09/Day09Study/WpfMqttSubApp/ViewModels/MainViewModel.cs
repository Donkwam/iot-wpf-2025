using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfMqttSubApp.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        private IMqttClient mqttClient;
        private readonly IDialogCoordinator dialogCoordinator;
        private readonly DispatcherTimer timer;
        private int counter = 1; // TODO : 나중에 삭제필요

        private string _brokerHost;
        private string _databaseHost;
        private string _logText;

        // 속성 BrokerHost, DatabaseHost
        // 메서드 ConnectBrokerCommand, ConnectDatabaseCommand

        public MainViewModel(IDialogCoordinator coordinator)
        {
            this.dialogCoordinator = coordinator;

            BrokerHost = "210.119.12.83";
            DatabaseHost = "210.119.12.83";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                // RichTextBox 추가 내용
                LogText += $"Log [{DateTime.Now:HH:mm:ss}] - {counter++}\n";
                //Debug.WriteLine($"Log [{DateTime.Now:HH:mm:ss}] - {counter++}");
            };
            timer.Start();
        }

        public string LogText
        {
            get => _logText;
            set => SetProperty(ref _logText, value);
        }

        public MainViewModel()
        {
        }

        public string BrokerHost
        {
            get => _brokerHost;
            set => SetProperty(ref _brokerHost, value);
               
        }

        public string DatabaseHost
        {
            get => _databaseHost;
            set => SetProperty(ref _databaseHost, value);
        }
        private async Task ConnectMqttBroker()
        {
            // MQTT 클라이언트 생성
            var mqttFactory = new MqttClientFactory();
            mqttClient = mqttFactory.CreateMqttClient();

            // MQTT 클라이언트접속 설정
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(BrokerHost)
                .WithCleanSession(true)
                .Build();

            //MQTT 접속 후 이벤트처리
            mqttClient.ConnectedAsync += async e =>
            {

            };
        }

        [RelayCommand]
        public async Task ConnectBroker()
        {
            if (string.IsNullOrEmpty(BrokerHost))
            { 
                await this.dialogCoordinator.ShowMessageAsync(this, "브로커연결", "브로커호스트를 입력하세요");
                return;
            }

            // MQTT브로커에 접속해서 데이터를 가져오기
            ConnectMqttBroker();
        }


        [RelayCommand]
        public async Task ConnectDatabase()
        {
            await this.dialogCoordinator.ShowMessageAsync(this, "DB연결", "DB 연결합니다!");
        }
    }
}
