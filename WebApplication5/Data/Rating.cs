using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class Rating
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int Rating1 { get; set; }

    public string Comment { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
