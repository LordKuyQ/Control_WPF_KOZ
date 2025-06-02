using System;
using System.ComponentModel.DataAnnotations;

namespace Control_WPF_KOZ.Models
{
    public partial class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название жанра обязательно.")]
        [StringLength(50, ErrorMessage = "Название жанра не может быть длиннее 50 символов.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Описание жанра обязательно.")]
        public string Description { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
