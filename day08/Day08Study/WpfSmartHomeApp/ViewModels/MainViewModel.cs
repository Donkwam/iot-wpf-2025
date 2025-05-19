using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSmartHomeApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private double _homeTemp;
        // 온도 속성
        public double HomeTemp
        {
            get => _homeTemp;
            set => SetProperty(ref _homeTemp, value);
        }

        private double _homeHumid;

        // 습도 속성
        public double HomeHumid
        {
            get => _homeHumid;
            set => SetProperty(ref _homeHumid, value);
        }

        private string _detectResult;
        // 사람인지
        public string DetectResult
        {
            get => _detectResult;
            set => SetProperty(ref _detectResult, value);
        }

        private string _isDetecOn;
        // 사람인지 여부
        public string IsDetecOn
        {
            get => _isDetecOn;
            set => SetProperty(ref _isDetecOn, value);
        }

        private string _rainResult;
        // 비
        public string RainResult
        {
            get => _rainResult;
            set => SetProperty(ref _rainResult, value);
        }

        private string _isRainon;
        // 비인지 여부
        public string IsRainon
        {
            get => _isRainon;
            set => SetProperty(ref _isRainon, value);
        }

        private string _airConResult;
        // 에어컨인지
        public string AirConResult
        {
            get => _airConResult;
            set => SetProperty(ref _airConResult, value);
        }

        private string _isAirConOn;
        // 에어컨인지 여부
        public string IsAirConOn
        {
            get => _isAirConOn;
            set => SetProperty(ref _isAirConOn, value);
        }

        private string _lightResult;
        // 빛
        public string LightResult
        {
            get => _lightResult;
            set => SetProperty(ref _lightResult, value);
        }

        private string _isLightOn;
        // 빛인지 여부
        public string IsLightOn
        {
            get => _isLightOn;
            set => SetProperty(ref _isLightOn, value);
        }

        // LoadedCommand 에서 On이 붙고 Command는 삭제
        [RelayCommand]
        public void OnLoaded()
        {
            HomeTemp = 30;
            HomeHumid = 43.2;

            DetectResult = "Detected Human!";
            IsDetecOn = "true";
            RainResult = "Raining";
            IsRainon = "true";
            AirConResult = "Aircon On!";
            IsAirConOn = "true";
            LightResult = "Light On!";
            IsLightOn = "true";
        }
    }
}
