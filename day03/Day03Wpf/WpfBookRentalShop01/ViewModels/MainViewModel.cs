using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfBookRentalShop01.Views;

namespace WpfBookRentalShop01.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private string _greeting;

        public string Greeting
        {
            get => _greeting;
            set => SetProperty(ref _greeting, value);
        }

        private string _currentView;

        public string CurrentStatus
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        private UserControl currentView;

        public UserControl CurrentView
        {
            get => currentView;
            set => SetProperty(ref currentView, value);
        }

        public MainViewModel()
        {
            Greeting = "BookRentalShop!!";
        }

        #region '화면 기능(이벤트)처리'  

        [RelayCommand]
        public void AppExit()
        {
            MessageBox.Show("종료합니다!");
        }

        [RelayCommand]
        public void ShowBookGenre()
        {
            var vm = new BookGenreViewModel();
            var v = new BookGenreView
            {
                DataContext = vm,
            };
            CurrentView = v;
            CurrentStatus = "책장르관리 화면입니다.";
        }

        [RelayCommand]
        public void ShowBooks()
        {
            var vm = new BooksViewModel();
            var v = new BooksView
            {
                DataContext = vm,
            };
            CurrentView = v;
            CurrentStatus = "책관리 화면입니다.";

        }

        #endregion
    }
}
