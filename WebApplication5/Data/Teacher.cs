using System;
using System.Collections.Generic;

namespace WebApplication5.Data;

public partial class Teacher
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public int? PhotoUrlId { get; set; }

    public virtual Photo? PhotoUrl { get; set; }

    public virtual ICollection<TeachersSpecialization> TeachersSpecializations { get; set; } = new List<TeachersSpecialization>();
}
