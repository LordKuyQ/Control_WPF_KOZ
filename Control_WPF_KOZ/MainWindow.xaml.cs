using Control_WPF_KOZ.Auto;
using Control_WPF_KOZ.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Control_WPF_KOZ
{
    public partial class MainWindow : Window
    {
        private DataBaseContext _context;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            AutUser autUser = new AutUser();

            autUser.ShowDialog();

            if (autUser.DialogResult == false)
            {
                Close();
            }
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                _context = new DataBaseContext();
                var books = _context.Books
                        .Include(b => b.Genre)
                        .Include(b => b.Status)
                        .ToList();

                var users = _context.Users.ToList();

                var bookViewModels = books.Select(b => new BookViewModel
                {
                    Articul = b.Id,
                    Name = b.Name,
                    GenreName = b.Genre?.Name,
                    Description = b.Description,
                    Date_publish = b.DateRelease.Date.ToString(),
                    StatusContent = b.Status?.Content,
                    SelectedUserId = b.UserId,
                    UserName = b.User?.Fio
                }).ToList();

                datagrid_books.ItemsSource = bookViewModels;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_add_book_Click(object sender, RoutedEventArgs e)
        {
            var addBookWindow = new AddBookWindow();
            if (addBookWindow.ShowDialog() == true)
            {
                try
                {
                    var validationContext = new ValidationContext(addBookWindow.NewBook);
                    var validationResults = new List<ValidationResult>();
                    bool isValid = Validator.TryValidateObject(addBookWindow.NewBook, validationContext, validationResults, true);

                    if (isValid)
                    {
                        _context.Books.Add(addBookWindow.NewBook!);
                        _context.SaveChanges();
                        LoadData();
                    }
                    else
                    {
                        string errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                        MessageBox.Show($"Ошибка валидации: {errorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void button_del_user_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid_books.SelectedItem is BookViewModel selectedBook)
            {
                try
                {
                    var bookToUpdate = _context.Books.Find(selectedBook.Articul);
                    if (bookToUpdate != null)
                    {
                        bookToUpdate.UserId = null;
                        _context.SaveChanges();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_upd_user_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid_books.SelectedItem is BookViewModel selectedBookViewModel)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == selectedBookViewModel.Articul);
                if (book != null)
                {
                    var updUserWindow = new UpdUserWindow(book);
                    if (updUserWindow.ShowDialog() == true)
                    {
                        LoadData(); 
                    }
                }
                else
                {
                    MessageBox.Show("Книга не найдена");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу");
            }
        }


        private void button_upd_status_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid_books.SelectedItem is BookViewModel selectedBookViewModel)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == selectedBookViewModel.Articul);
                if (book != null)
                {
                    var updStatusWindow = new Auto.UpdStatusWindow(book);
                    if (updStatusWindow.ShowDialog() == true)
                    {
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Книга не найдена");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу");
            }
        }

        private void button_del_book_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid_books.SelectedItem is BookViewModel selectedBookViewModel)
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == selectedBookViewModel.Articul);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Книга не найдена");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу");
            }
        }
    }
}
