using System;
using System.Collections.Generic;

namespace Control_WPF_KOZ.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
