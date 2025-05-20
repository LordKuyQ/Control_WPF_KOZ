using System;
using System.Collections.Generic;

namespace Control_WPF_KOZ.Models;

public partial class Book
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int GenreId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateRelease { get; set; }

    public int StatusId { get; set; }

    public int? UserId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
