using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class DifficultyLevel
{
    public int Id { get; set; }

    public string DifLevel { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
