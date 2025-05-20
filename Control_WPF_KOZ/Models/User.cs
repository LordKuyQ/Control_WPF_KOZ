using System;
using System.Collections.Generic;

namespace Control_WPF_KOZ.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateReristr { get; set; }

    public string Fio { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
