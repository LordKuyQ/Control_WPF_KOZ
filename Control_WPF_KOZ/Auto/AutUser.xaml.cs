using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Control_WPF_KOZ.Models;
using Microsoft.EntityFrameworkCore;

namespace Control_WPF_KOZ.Auto
{
    public partial class AutUser : Window
    {
        private DataBaseContext _context;
        public User AuthenticatedUser { get; private set; }

        public AutUser()
        {
            InitializeComponent();
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

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = BoxName.Text;
            string password = BoxPass.Text;

            try
            {
                using (_context = new DataBaseContext())
                {
                    var user = _context.Users
                        .FirstOrDefault(q => q.Login == login);

                    if (user != null && VerifyHashedPassword(user.Password, password))
                    {
                        AuthenticatedUser = user;
                        MessageBox.Show("Вы вошли");
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль не верны");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            var regUser = new RegUser();
            regUser.ShowDialog();

            if (regUser.DialogResult == true)
            {
                using (_context = new DataBaseContext())
                {
                    regUser.NewUser.Password = HashPassword(regUser.NewUser.Password);
                    _context.Users.Add(regUser.NewUser);
                    _context.SaveChanges();
                }
            }
        }
    }
}
