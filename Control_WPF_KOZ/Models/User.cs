using System;
using System.ComponentModel.DataAnnotations;

namespace Control_WPF_KOZ.Models
{
    public partial class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан логин пользователя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина логина")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Не указан пароль пользователя")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Не указана дата регистрации пользователя")]
        public DateTime DateReristr { get; set; }

        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Fio { get; set; } = null!;

        [Phone(ErrorMessage = "Недопустимый формат телефона")]
        public string Phone { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
