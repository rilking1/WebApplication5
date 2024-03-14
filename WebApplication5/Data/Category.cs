using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class Category
{
    public int Id { get; set; }

    public string Category1 { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
