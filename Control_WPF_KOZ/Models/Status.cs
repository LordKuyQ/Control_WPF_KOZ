using System;
using System.ComponentModel.DataAnnotations;

namespace Control_WPF_KOZ.Models
{
    public partial class Status
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Содержимое статуса обязательно.")]
        public string Content { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
