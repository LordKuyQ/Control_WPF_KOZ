using Control_WPF_KOZ.Models;
using System.Windows;

namespace Control_WPF_KOZ
{
    public partial class AddBookWindow : Window
    {
        public Book? NewBook { get; private set; }

        private DataBaseContext _context;

        public AddBookWindow()
        {
            InitializeComponent();
            _context = new DataBaseContext();

            GenreComboBox.ItemsSource = _context.Genres.ToList();
            StatusComboBox.ItemsSource = _context.Statuses.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите название книги");
                return;
            }

            if (GenreComboBox.SelectedItem is not Genre selectedGenre ||
                StatusComboBox.SelectedItem is not Status selectedStatus)
            {
                MessageBox.Show("Выберите жанр и статус");
                return;
            }

            if (DateReleasePicker.SelectedDate is not DateTime selectedDate)
            {
                MessageBox.Show("Выберите дату издания");
                return;
            }

            NewBook = new Book
            {
                Id = ArticulTextBox.Text,
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                GenreId = selectedGenre.Id,
                StatusId = selectedStatus.Id,
                DateRelease = selectedDate
            };

            DialogResult = true;
            Close();
        }
    }
}
