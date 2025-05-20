using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMqttSubApp.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        private readonly IDialogCoordinator dialogCoordinator;
        private string _brokerHost;
        private string _databaseHost;

        // 속성 BrokerHost, DatabaseHost
        // 메서드 ConnectBrokerCommand, ConnectDatabaseCommand

        public MainViewModel(IDialogCoordinator coordinator)
        {
            this.dialogCoordinator = coordinator;

            BrokerHost = "210.119.12.83";
            DatabaseHost = "210.119.12.83";
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

        [RelayCommand]
        public async Task ConnectBroker()
        {
            if (string.IsNullOrEmpty(BrokerHost))
            { 
                await this.dialogCoordinator.ShowMessageAsync(this, "브로커연결", "브로커호스트를 입력하세요");
                return;
            }

            // MQTT브로커에 접속해서 데이터를 가져오기
        }

        [RelayCommand]
        public async Task ConnectDatabase()
        {
            await this.dialogCoordinator.ShowMessageAsync(this, "DB연결", "DB 연결합니다!");
        }
    }
}
