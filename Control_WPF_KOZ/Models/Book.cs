using System;
using System.ComponentModel.DataAnnotations;

namespace Control_WPF_KOZ.Models
{
    public partial class Book
    {
        [Required(ErrorMessage = "ID книги обязателен.")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Название книги обязательно.")]
        [StringLength(100, ErrorMessage = "Название книги не может быть длиннее 100 символов.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "ID жанра обязателен.")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Описание книги обязательно.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Дата выпуска обязательна.")]
        public DateTime DateRelease { get; set; }

        [Required(ErrorMessage = "ID статуса обязателен.")]
        public int StatusId { get; set; }

        public int? UserId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
