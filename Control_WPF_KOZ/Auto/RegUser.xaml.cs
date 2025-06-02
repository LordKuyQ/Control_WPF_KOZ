using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
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

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
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
                    Phone = BoxTelefon.Text,
                    DateReristr = DateTime.Now
                };

                var validationContext = new ValidationContext(NewUser);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(NewUser, validationContext, validationResults, true);

                if (isValid)
                {
                    DialogResult = true;
                }
                else
                {
                    string errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                    MessageBox.Show($"Ошибка валидации: {errorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
