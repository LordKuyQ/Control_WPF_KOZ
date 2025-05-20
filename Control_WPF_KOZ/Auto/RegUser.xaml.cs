using System;
using System.Linq;
using System.Windows;
using Control_WPF_KOZ.Models;

namespace Control_WPF_KOZ.Auto
{
    public partial class RegUser : Window
    {
        public User NewUser { get; private set; }
        private DataBaseContext _context;

        public RegUser()
        {
            InitializeComponent();
            _context = new DataBaseContext();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BoxName.Text) && !string.IsNullOrEmpty(BoxPass.Text) && !string.IsNullOrEmpty(BoxLogin.Text))
            {
                if (_context.Users.Any(c => c.Login == BoxLogin.Text))
                {
                    MessageBox.Show("Этот логин уже используется. Пожалуйста, выберите другой логин.");
                    return;
                }

                NewUser = new User
                {
                    Login = BoxLogin.Text,
                    Password = BoxPass.Text,
                    Fio = BoxName.Text,
                    Phone = BoxTelefon.Text
                };

                DialogResult = true;
                return;
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
