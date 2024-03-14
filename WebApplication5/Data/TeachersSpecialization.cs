using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class TeachersSpecialization
{
    public int Id { get; set; }

    public int TeachersId { get; set; }

    public int SpecializationId { get; set; }

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual Teacher Teachers { get; set; } = null!;
}
