using System;
using System.Collections.Generic;

namespace apiHorus.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Name { get; set; }

    public virtual Role Role { get; set; } = null!;
}
