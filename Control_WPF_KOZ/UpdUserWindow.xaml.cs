using Control_WPF_KOZ.Models;
using System.Windows;

namespace Control_WPF_KOZ
{
    public partial class UpdUserWindow : Window
    {
        private readonly DataBaseContext _context;
        private readonly Book _book;

        public UpdUserWindow(Book book)
        {
            InitializeComponent();
            _context = new DataBaseContext();
            _book = book;

            var users = _context.Users.ToList();
            combobox_users.ItemsSource = users;
            combobox_users.DisplayMemberPath = "Fio";

            if (_book.UserId != null)
            {
                combobox_users.SelectedItem = users.FirstOrDefault(u => u.Id == _book.UserId);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_users.SelectedItem is User selectedUser)
            {
                var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == _book.Id);
                if (bookToUpdate != null)
                {
                    bookToUpdate.UserId = selectedUser.Id;
                    _context.SaveChanges();
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Книга не найдена");
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }

            Close();
        }
    }
}
