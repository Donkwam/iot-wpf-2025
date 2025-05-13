using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using WpfBookRentalShop01.Helpers;
using WpfBookRentalShop01.Models;

namespace WpfBookRentalShop01.ViewModels
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly IDialogCoordinator dialogCoordinator;

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetProperty(ref _selectedBook, value);
                IsUpdate = true;
            }
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get => _isUpdate;
            set => SetProperty(ref _isUpdate, value);
        }

        public BooksViewModel(IDialogCoordinator coordinator)
        {
            this.dialogCoordinator = coordinator;
            LoadBooksFromDb();
        }

        private void InitVariable()
        {
            SelectedBook = new Book
            {
                Idx = 0,
                Author = string.Empty,
                Division = string.Empty,
                Names = string.Empty,
                ReleaseDate = string.Empty,
                ISBN = string.Empty,
                Price = 0
            };
            IsUpdate = false;
        }

        [RelayCommand]
        public void SetInit()
        {
            InitVariable();
        }

        [RelayCommand]
        public async void SaveData()
        {
            try
            {
                string query;
                using var conn = new MySqlConnection(Common.CONNSTR);
                conn.Open();

                if (_isUpdate)
                {
                    query = @"UPDATE bookstbl SET 
                                Author = @Author, Division = @Division, Names = @Names,
                                ReleaseDate = @ReleaseDate, ISBN = @ISBN, Price = @Price
                              WHERE Idx = @Idx";
                }
                else
                {
                    query = @"INSERT INTO bookstbl (Author, Division, Names, ReleaseDate, ISBN, Price)
                              VALUES (@Author, @Division, @Names, @ReleaseDate, @ISBN, @Price)";
                }

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Author", SelectedBook.Author);
                cmd.Parameters.AddWithValue("@Division", SelectedBook.Division);
                cmd.Parameters.AddWithValue("@Names", SelectedBook.Names);
                cmd.Parameters.AddWithValue("@ReleaseDate", SelectedBook.ReleaseDate);
                cmd.Parameters.AddWithValue("@ISBN", SelectedBook.ISBN);
                cmd.Parameters.AddWithValue("@Price", SelectedBook.Price);
                if (_isUpdate) cmd.Parameters.AddWithValue("@Idx", SelectedBook.Idx);

                int resultCnt = cmd.ExecuteNonQuery();
                await dialogCoordinator.ShowMessageAsync(this, "저장결과", resultCnt > 0 ? "성공!" : "실패!");

                LoadBooksFromDb();
                InitVariable();
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                await dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }
        }

        [RelayCommand]
        public async void DeleteData()
        {
            if (!_isUpdate)
            {
                await dialogCoordinator.ShowMessageAsync(this, "삭제", "삭제할 항목을 선택하세요.");
                return;
            }

            var result = await dialogCoordinator.ShowMessageAsync(this, "삭제확인", "정말 삭제하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative);
            if (result != MessageDialogResult.Affirmative) return;

            try
            {
                string query = "DELETE FROM bookstbl WHERE Idx = @Idx";
                using var conn = new MySqlConnection(Common.CONNSTR);
                conn.Open();

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Idx", SelectedBook.Idx);

                int resultCnt = cmd.ExecuteNonQuery();
                await dialogCoordinator.ShowMessageAsync(this, "삭제결과", resultCnt > 0 ? "삭제 성공" : "삭제 실패");

                LoadBooksFromDb();
                InitVariable();
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                await dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }
        }

        private async void LoadBooksFromDb()
        {
            try
            {
                ObservableCollection<Book> books = new();
                using var conn = new MySqlConnection(Common.CONNSTR);
                conn.Open();

                string query = "SELECT Idx, Author, Division, Names, ReleaseDate, ISBN, Price FROM bookstbl";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Idx = reader.GetInt32("Idx"),
                        Author = reader.GetString("Author"),
                        Division = reader.GetString("Division"),
                        Names = reader.GetString("Names"),
                        ReleaseDate = reader.GetString("ReleaseDate"),
                        ISBN = reader.GetString("ISBN"),
                        Price = reader.GetInt32("Price")
                    });
                }

                Books = books;
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                await dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }
        }
    }
}
