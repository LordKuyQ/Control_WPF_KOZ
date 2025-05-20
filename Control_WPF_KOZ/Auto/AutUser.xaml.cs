using System;
using System.Linq;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string login = BoxName.Text;
            string password = BoxPass.Text;

            try
            {
                using (_context = new DataBaseContext())
                {
                    var user = _context.Users
                        .FirstOrDefault(q => q.Login == login && q.Password == password);

                    if (user != null)
                    {
                        AuthenticatedUser = user;
                        MessageBox.Show($"Вы вошли");
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
                    _context.Users.Add(regUser.NewUser);
                    _context.SaveChanges();
                }
            }
        }
    }
}
