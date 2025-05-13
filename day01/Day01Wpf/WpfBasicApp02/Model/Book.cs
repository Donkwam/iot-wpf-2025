using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBasicApp02.Model
{
    public class Book : INotifyPropertyChanged
    {
        public int Idx { get; set; }
        public string Division { get; set; }
        public string DNames { get; set; }

        private string _names;
        public string Names { 
            get => _names;
            set {
                _names = value;
                OnPropertyChanged(nameof(Names));
            }
        }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }

        // 위의 여덟개의 값이 기존상태에서 변경이되면 발생하는 이벤트
        public event PropertyChangedEventHandler? PropertyChanged; // 사용자가 클릭같은거로 발생하는 이벤트가 아님

        protected void OnPropertyChanged(string name)
        {
            // 기본적인 이벤트핸들러 파라미터와 동일(object sender, EventArgs e)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
