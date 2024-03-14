using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class Specialization
{
    public int Id { get; set; }

    public string Specialization1 { get; set; } = null!;

    public virtual ICollection<TeachersSpecialization> TeachersSpecializations { get; set; } = new List<TeachersSpecialization>();
}
