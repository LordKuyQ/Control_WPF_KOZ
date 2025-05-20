using Control_WPF_KOZ.Models;
using System.Windows;

namespace Control_WPF_KOZ.Auto
{
    public partial class UpdStatusWindow : Window
    {
        private readonly DataBaseContext _context;
        private readonly Book _book;

        public UpdStatusWindow(Book book)
        {
            InitializeComponent();
            _context = new DataBaseContext();
            _book = book;

            var statuses = _context.Statuses.ToList();
            combobox_statuses.ItemsSource = statuses;

            if (_book.StatusId != null)
            {
                combobox_statuses.SelectedItem = statuses.FirstOrDefault(s => s.Id == _book.StatusId);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (combobox_statuses.SelectedItem is Status selectedStatus)
            {
                var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == _book.Id);
                if (bookToUpdate != null)
                {
                    bookToUpdate.StatusId = selectedStatus.Id;
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
                MessageBox.Show("Выберите статус");
            }

            Close();
        }
    }
}
