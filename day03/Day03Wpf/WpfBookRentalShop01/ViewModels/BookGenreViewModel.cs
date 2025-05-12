using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfBookRentalShop01.Models;

namespace WpfBookRentalShop01.ViewModels
{
    public partial class BookGenreViewModel : ObservableObject
    {
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres { 
            get => _genres; 
            set => SetProperty(ref _genres, value);
        }

        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                SetProperty(ref _selectedGenre, value);
                _isUpdate = true;
            }
        }

        private bool _isUpdate;
        
        public BookGenreViewModel()
        {
            _isUpdate = false;
            LoadGridFromDb();
        }
        private void LoadGridFromDb()
        {
            try
            {
                string connStr = "Server=localhost;Database=bookrentalshop;Uid=root;Pwd=12345;Charset=utf8;";
                string query = "SELECT division, names FROM divtbl";
                ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var division = reader.GetString("division");
                        var names = reader.GetString("names");

                        genres.Add(new Genre
                        {
                            Division = division,
                            Names = names
                        });
                    }
                }

                Genres = genres;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand]
        public void Init() {
            _isUpdate = false;
            SelectedGenre = null;
        }
        [RelayCommand]
        public void Save() { 

        }
        [RelayCommand]
        public void Del() { 
            if (!_isUpdate) { MessageBox.Show("선택된 데이터가 아니면 삭제 할 수 없습니다."); return; }

            string connStr = "Server=localhost;Database=bookrentalshop;Uid=root;Pwd=root;Charset=utf8;";
            string query = "DELETE FROM divtbl WHERE division = @division";
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@division", SelectedGenre.Division);
                int resultCount = cmd.ExecuteNonQuery(); // 성공 : 1 리턴

            }
        }
    }
}
