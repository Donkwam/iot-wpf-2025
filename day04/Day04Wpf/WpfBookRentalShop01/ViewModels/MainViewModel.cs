using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfBookRentalShop01.Helpers;
using WpfBookRentalShop01.Views;

namespace WpfBookRentalShop01.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // MahApps.Metro 형태 다이얼로그 코디네이터
        private readonly IDialogCoordinator dialogCoordinator;

        private string _greeting;
        public string Greeting { 
            get => _greeting;             
            set => SetProperty(ref _greeting, value); 
        }

        public MainViewModel(IDialogCoordinator coordinator)
        {
            this.dialogCoordinator = coordinator; // 다이얼로그코디네이터 초기화
            Greeting = "BookRentalShop!!";

            Common.LOGGER.Info("책대여점 프로그램 실행!");
        }

        private UserControl _currentview;
        private string _currentStatus;
        public UserControl Currentview { 
            get => _currentview; 
            set => SetProperty(ref _currentview, value); 
        }
        public string CurrentStatus { 
            get => _currentStatus; 
            set => SetProperty(ref _currentStatus, value); 
        }


        #region 화면 기능(이벤트처리)

        [RelayCommand]
        public async Task AppExit()
        {
            //MessageBox.Show("ㅇㅇ", "ㄴㄴ");
            //await this.dialogCoordinator.ShowMessageAsync(this, "종료합니다!", "메시지");
            var result = await this.dialogCoordinator.ShowMessageAsync(this, "종료확인", "종료하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }
            else 
            {
                return;
            }
        }

        [RelayCommand]
        public void ShowBookGenre()
        {
            var viewModel = new BookGenreViewModel(Common.DIALOGCOORDINATOR);
            var view = new BookGenreView();
            view.DataContext = viewModel;
            CurrentStatus = "책장르 관리 화면 입니다.";
            Currentview = view;
            Common.LOGGER.Info("책장르관리 실행");
        }

        [RelayCommand]
        public void ShowBooks()
        {
            var viewModel = new BooksViewModel();
            var view = new BooksView();
            view.DataContext = viewModel;
            CurrentStatus = "책 관리 화면 입니다.";
            Currentview = view;
            Common.LOGGER.Info("책관리 실행");

        }
        #endregion
    }
}
